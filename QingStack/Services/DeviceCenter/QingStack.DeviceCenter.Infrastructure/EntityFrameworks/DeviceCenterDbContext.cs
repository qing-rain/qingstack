/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceCenterDbContext.cs
    文件功能描述：设备中心上下文


    创建标识：QingRain - 20211109

    修改标识: QingRain - 20211110
    修改描述：1、增加多租户、软删除接口实现处理逻辑2、重写SaveChangesAsync

    修改标识: QingRain - 20211110
    修改描述：1、增加MediatR注入
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using QingStack.DeviceCenter.Domain.Entities;
using QingStack.DeviceCenter.Domain.UnitOfWork;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public DeviceCenterDbContext(DbContextOptions<DeviceCenterDbContext> options) : base(options) => _mediator = this.GetInfrastructure().GetService<IMediator>() ?? new NullMediator();
        /// <summary>
        /// 隐式实现
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => await base.SaveChangesAsync(cancellationToken);

        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //执行删除且支持软删除
            var deletedEntries = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Deleted && entry.Entity is ISoftDelete);

            //?不为空遍历
            deletedEntries?.ToList().ForEach(entityEntry =>
            {
                //加在最新数据
                entityEntry.Reload();
                //更新为修改状态
                entityEntry.State = EntityState.Modified;
                ((ISoftDelete)entityEntry.Entity).IsDeleted = true;
            });
            //触发领域事件
            await DispatchDomainEventsAsync(cancellationToken);

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        /// <summary>
        /// 触发领域事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            //继承BaseEntity 同时实现了IDomainEvents
            var domainEntities = ChangeTracker.Entries<BaseEntity>().OfType<IDomainEvents>();
            var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();
            domainEntities.ToList().ForEach(entity => entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///扫描当前执行程序集所有配置
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                //实现多租户接口
                if (entityType.ClrType.IsAssignableTo(typeof(IMultiTenant)))
                {
                    //当前上下文服务
                    ICurrentTenant? currentTenant = this.GetInfrastructure().GetService<ICurrentTenant>();
                    if (currentTenant?.Id is not null)
                    {
                        modelBuilder.Entity(entityType.ClrType).AddQueryFilter<IMultiTenant>(e => e.TenantId == currentTenant.Id);
                    }
                }
                //实现软删除接口
                if (entityType.ClrType.IsAssignableTo(typeof(ISoftDelete)))
                {
                    modelBuilder.Entity(entityType.ClrType).AddQueryFilter<ISoftDelete>(e => !e.IsDeleted);
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
