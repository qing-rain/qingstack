/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：RequestManager.cs
    文件功能描述：基于数据库请求管理器实现


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Infrastructure.EntityFrameworks;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Idempotency
{
    public class RequestManager : IRequestManager
    {
        private readonly DeviceCenterDbContext _dbContext;

        public RequestManager(DeviceCenterDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<bool> ExistAsync(string id)
        {
            ClientRequest? request = await _dbContext.FindAsync<ClientRequest>(id);

            return request != null;
        }

        public async Task CreateRequestForCommandAsync<T>(string id)
        {
            if (await ExistAsync(id))
            {
                throw new ApplicationException($"Request with {id} already exists");
            }

            var request = new ClientRequest() { Id = id, Name = typeof(T).Name, Time = DateTimeOffset.Now };

            _dbContext.Add(request);

            await _dbContext.SaveChangesAsync();
        }
    }
}
