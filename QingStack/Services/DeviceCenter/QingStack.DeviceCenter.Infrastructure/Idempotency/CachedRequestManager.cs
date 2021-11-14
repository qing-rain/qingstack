/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CachedRequestManager.cs
    文件功能描述：基于分布式缓存请求管理器实现


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Idempotency
{
    public class CachedRequestManager : IRequestManager
    {
        private readonly IDistributedCache _cache;

        public CachedRequestManager(IDistributedCache cache) => _cache = cache;

        public async Task<bool> ExistAsync(string id) => (await _cache.GetStringAsync(id)) is not null;

        public async Task CreateRequestForCommandAsync<T>(string id)
        {
            if (await ExistAsync(id))
            {
                throw new ApplicationException($"Request with {id} already exists");
            }

            var request = new ClientRequest() { Id = id, Name = typeof(T).Name, Time = DateTimeOffset.Now };


            await _cache.SetStringAsync(id, JsonSerializer.Serialize(request));
        }
    }
}
