/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceCenterDbContext.cs
    文件功能描述：设备中心上下文


    创建标识：QingRain - 20211109

    修改标识: QingRain - 20211110
    修改描述：1、增加多租户、软删除接口实现处理逻辑2、重写SaveChangesAsync

    修改标识: QingRain - 20211110
    修改描述：1、增加MediatR注入

    修改标识: QingRain - 20211114
    修改描述：使用保存变更拦截器横切关注点

    修改标识: QingRain - 20211114
    修改描述：多租户注入服务获取方式调整

    修改标识: QingRain - 20211212
    修改描述：重构并修复多租户数据筛选器
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using QingStack.DeviceCenter.Domain.Entities;
using QingStack.DeviceCenter.Domain.UnitOfWork;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterDbContext : DbContext, IUnitOfWork
    {
        public DeviceCenterDbContext(DbContextOptions<DeviceCenterDbContext> options) : base(options) { }
        /// <summary>
        /// 隐式实现
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => await base.SaveChangesAsync(cancellationToken);
        /// <summary>
        /// DbContext池模式只调用一次
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            foreach (IMutableEntityType entityType in
            modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsAssignableTo(typeof(IMultiTenant)))
                {
                    modelBuilder.Entity(entityType.ClrType).AddQueryFilter<IMultiTenant>(e =>
                    e.TenantId == this.GetService<ICurrentTenant>().Id);
                }
                if (entityType.ClrType.IsAssignableTo(typeof(ISoftDelete)))
                {

                    modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ISoftDelete>(e => !
                    e.IsDeleted);
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
