/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionStore.cs
    文件功能描述：权限审批存储

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionStore : IPermissionStore
    {
        private readonly IPermissionGrantRepository _repository;

        private readonly IPermissionDefinitionManager _permissionDefinitionManager;
        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IDistributedCache _distributedCache;

        private readonly ILogger<PermissionStore> _logger;
        /// <summary>
        /// 权限缓存Key(权限名：主体Key：主体类型)
        /// </summary>
        private const string CacheKeyFormat = "pn:{0},pk:{1},n:{2}"; //<object-type>:<id>:<field1>.<field2> Or <object-type>:<id>:<field1>-<field2> 模仿redis定义形式

        public PermissionStore(IPermissionGrantRepository repository, IPermissionDefinitionManager permissionDefinitionManager, IDistributedCache distributedCache, ILogger<PermissionStore> logger)
        {
            _repository = repository;
            _permissionDefinitionManager = permissionDefinitionManager;
            _distributedCache = distributedCache;
            //空日志注入
            _logger = logger ?? NullLogger<PermissionStore>.Instance;
        }
        /// <summary>
        /// 是否权限通过
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public async Task<bool> IsGrantedAsync([NotNull] string name, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            return (await GetCacheItemAsync(name, providerName, providerKey)).IsGranted;
        }
        /// <summary>
        /// 多权限是否通过
        /// </summary>
        /// <param name="names"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public async Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            if (names is null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            MultiplePermissionGrantResult result = new();

            if (names.Length == 1)
            {
                var name = names.First();
                result.Result.Add(name, await IsGrantedAsync(names.First(), providerName, providerKey) ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined);
                return result;
            }

            var cacheItems = await GetCacheItemsAsync(names, providerName, providerKey);

            foreach (var (Key, IsGranted) in cacheItems)
            {
                result.Result.Add(GetPermissionInfoFormCacheKey(Key).Name, IsGranted ? PermissionGrantResult.Granted : PermissionGrantResult.Undefined);
            }

            return result;
        }
        /// <summary>
        /// 查询缓存授权结果
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        protected virtual async Task<(string Key, bool IsGranted)> GetCacheItemAsync(string name, string providerName, string providerKey)
        {
            var cacheKey = string.Format(CacheKeyFormat, providerName, providerKey, name);

            _logger.LogDebug($"PermissionStore.GetCacheItemAsync: {cacheKey}");

            string cacheItem = await _distributedCache.GetStringAsync(cacheKey);

            if (cacheItem is not null)
            {
                _logger.LogDebug($"Found in the cache: {cacheKey}");
                return (cacheKey, Convert.ToBoolean(cacheItem));
            }

            _logger.LogDebug($"Not found in the cache: {cacheKey}");
            //缓存无结果
            return await SetCacheItemsAsync(providerName, providerKey, name);
        }
        /// <summary>
        /// 查询权限授予，设置缓存
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="currentName"></param>
        /// <returns></returns>
        protected virtual async Task<(string Key, bool IsGranted)> SetCacheItemsAsync(string providerName, string providerKey, string currentName)
        {
            var permissions = _permissionDefinitionManager.GetPermissions();

            _logger.LogDebug($"Getting all granted permissions from the repository for this provider name,key: {providerName},{providerKey}");

            var grantedPermissionsHashSet = new HashSet<string>((await _repository.GetListAsync(providerName, providerKey)).Select(p => p.Name));

            _logger.LogDebug($"Setting the cache items. Count: {permissions.Count}");

            var cacheItems = new List<(string Key, bool IsGranted)>();

            bool currentResult = false;

            foreach (var permission in permissions)
            {
                var isGranted = grantedPermissionsHashSet.Contains(permission.Name);

                cacheItems.Add((string.Format(CacheKeyFormat, providerName, providerKey, permission.Name), isGranted));

                if (permission.Name == currentName)
                {
                    currentResult = isGranted;
                }
            }

            List<Task> setCacheItemTasks = new();

            foreach ((string Key, bool IsGranted) in cacheItems)
            {
                setCacheItemTasks.Add(_distributedCache.SetStringAsync(Key, IsGranted.ToString()));
            }

            await Task.WhenAll(setCacheItemTasks);

            _logger.LogDebug($"Finished setting the cache items. Count: {permissions.Count}");

            return (string.Format(CacheKeyFormat, providerName, providerKey, currentName), currentResult);
        }
        /// <summary>
        /// 查询缓存多授权结果
        /// </summary>
        /// <param name="names"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        protected virtual async Task<List<(string Key, bool IsGranted)>> GetCacheItemsAsync(string[] names, string providerName, string providerKey)
        {
            var cacheKeys = names.Select(x => string.Format(CacheKeyFormat, providerName, providerKey, x)).ToList();

            _logger.LogDebug($"PermissionStore.GetCacheItemAsync: {string.Join(",", cacheKeys)}");

            List<Task<(string key, string value)>> getCacheItemTasks = new();

            foreach (string cacheKey in cacheKeys)
            {
                getCacheItemTasks.Add(Task.Run(() => (cacheKey, _distributedCache.GetStringAsync(cacheKey).Result)));
            }

            var cacheItems = await Task.WhenAll(getCacheItemTasks);

            if (cacheItems.All(x => x.value is not null))
            {
                _logger.LogDebug($"Found in the cache: {string.Join(",", cacheKeys)}");
                return Array.ConvertAll(cacheItems, i => (i.key, Convert.ToBoolean(i.value))).ToList();
            }

            var notCacheKeys = cacheItems.Where(x => x.value is null).Select(x => x.key).ToList();

            _logger.LogDebug($"Not found in the cache: {string.Join(",", notCacheKeys)}");

            return await SetCacheItemsAsync(providerName, providerKey, notCacheKeys);
        }

        protected virtual async Task<List<(string Key, bool IsGranted)>> SetCacheItemsAsync(string providerName, string providerKey, List<string> notCacheKeys)
        {
            var permissions = _permissionDefinitionManager.GetPermissions().Where(x => notCacheKeys.Any(k => GetPermissionInfoFormCacheKey(k).Name == x.Name)).ToList();

            _logger.LogDebug($"Getting not cache granted permissions from the repository for this provider name,key: {providerName},{providerKey}");

            var grantedPermissionsHashSet = new HashSet<string>((await _repository.GetListAsync(notCacheKeys.Select(k => GetPermissionInfoFormCacheKey(k).Name).ToArray(), providerName, providerKey)).Select(p => p.Name));

            _logger.LogDebug($"Setting the cache items. Count: {permissions.Count}");

            var cacheItems = new List<(string Key, bool IsGranted)>();

            foreach (PermissionDefinition? permission in permissions)
            {
                var isGranted = grantedPermissionsHashSet.Contains(permission.Name);
                cacheItems.Add((string.Format(CacheKeyFormat, providerName, providerKey, permission.Name), isGranted));
            }

            List<Task> setCacheItemTasks = new();

            foreach ((string Key, bool IsGranted) in cacheItems)
            {
                setCacheItemTasks.Add(_distributedCache.SetStringAsync(Key, IsGranted.ToString()));
            }

            await Task.WhenAll(setCacheItemTasks);

            _logger.LogDebug($"Finished setting the cache items. Count: {permissions.Count}");

            return cacheItems;
        }
        /// <summary>
        /// 解析缓存Key获取权限授予信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual (string ProviderName, string ProviderKey, string Name) GetPermissionInfoFormCacheKey(string key)
        {
            string pattern = @"^pn:(?<providerName>.+),pk:(?<providerKey>.+),n:(?<name>.+)$";

            Match match = Regex.Match(key, pattern, RegexOptions.IgnoreCase);

            string providerName = match.Groups["providerName"].Value;
            string providerKey = match.Groups["providerKey"].Value;
            string name = match.Groups["name"].Value;
            //元组类型
            return (providerName, providerKey, name);
        }
    }
}
