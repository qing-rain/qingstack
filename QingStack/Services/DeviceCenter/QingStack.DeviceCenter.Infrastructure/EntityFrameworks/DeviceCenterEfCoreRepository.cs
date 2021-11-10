/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceCenterEfCoreRepository.cs
    文件功能描述：设备中心EF仓储


    创建标识：QingRain - 20210114
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterEfCoreRepository<TEntity> : EfCoreRepository<DeviceCenterDbContext, TEntity> where TEntity : BaseEntity
    {
        public DeviceCenterEfCoreRepository(DeviceCenterDbContext dbContext) : base(dbContext)
        {

        }
    }

    public class DeviceCenterEfCoreRepository<TEntity, TKey> : EfCoreRepository<DeviceCenterDbContext, TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public DeviceCenterEfCoreRepository(DeviceCenterDbContext dbContext) : base(dbContext)
        {

        }
    }
}
