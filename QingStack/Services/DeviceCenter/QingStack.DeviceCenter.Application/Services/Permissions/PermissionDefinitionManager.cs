/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionDefinitionManager.cs
    文件功能描述：权限定义管理器


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDefinitionManager : IPermissionDefinitionManager
    {
        /// <summary>
        /// 延迟加载
        /// </summary>
        private readonly Lazy<Dictionary<string, PermissionGroupDefinition>> _lazyPermissionGroupDefinitions;
        /// <summary>
        /// 权限组定义列表
        /// </summary>
        protected IDictionary<string, PermissionGroupDefinition> PermissionGroupDefinitions => _lazyPermissionGroupDefinitions.Value;

        private readonly Lazy<Dictionary<string, PermissionDefinition>> _lazyPermissionDefinitions;
        /// <summary>
        /// 权限定义列表
        /// </summary>
        protected IDictionary<string, PermissionDefinition> PermissionDefinitions => _lazyPermissionDefinitions.Value;

        private readonly IServiceProvider _serviceProvider;

        public PermissionDefinitionManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _lazyPermissionDefinitions = new(CreatePermissionDefinitions, isThreadSafe: true);
            _lazyPermissionGroupDefinitions = new(CreatePermissionGroupDefinitions, isThreadSafe: true);
        }

        public PermissionDefinition Get([NotNull] string name)
        {
            var permission = GetOrNull(name);

            if (permission is null)
            {
                throw new InvalidOperationException($"Undefined permission {name}");
            }

            return permission;
        }

        public IReadOnlyList<PermissionGroupDefinition> GetGroups() => PermissionGroupDefinitions.Values.ToImmutableList();

        public PermissionDefinition? GetOrNull([NotNull] string name) => PermissionDefinitions.TryGetValue(name, out var obj) ? obj : default;

        public IReadOnlyList<PermissionDefinition> GetPermissions() => PermissionDefinitions.Values.ToImmutableList();

        protected virtual Dictionary<string, PermissionDefinition> CreatePermissionDefinitions()
        {
            var permissions = new Dictionary<string, PermissionDefinition>();

            foreach (var groupDefinition in PermissionGroupDefinitions.Values)
            {
                foreach (var permission in groupDefinition.Permissions)
                {
                    AddPermissionToDictionaryRecursively(permissions, permission);
                }
            }

            return permissions;
        }

        protected virtual void AddPermissionToDictionaryRecursively(Dictionary<string, PermissionDefinition> permissions, PermissionDefinition permission)
        {
            if (permissions.ContainsKey(permission.Name))
            {
                throw new InvalidOperationException($"Duplicate permission name {permission.Name}");
            }

            permissions[permission.Name] = permission;

            foreach (var child in permission.Children)
            {
                AddPermissionToDictionaryRecursively(permissions, child);
            }
        }

        protected virtual Dictionary<string, PermissionGroupDefinition> CreatePermissionGroupDefinitions()
        {
            using var scope = _serviceProvider.CreateScope();

            var context = new PermissionDefinitionContext(scope.ServiceProvider);

            var providers = _serviceProvider.GetServices<IPermissionDefinitionProvider>();

            foreach (IPermissionDefinitionProvider provider in providers)
            {
                provider.Define(context);
            }

            return context.Groups;
        }
    }
}
