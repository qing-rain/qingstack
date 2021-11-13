/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomSaveChangesInterceptor.cs
    文件功能描述：自定义保存拦截器


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using QingStack.DeviceCenter.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class CustomSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public CustomSaveChangesInterceptor(IMediator mediator) => _mediator = mediator;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            MultiTenancyTracking(eventData.Context);
            SoftDeleteTracking(eventData.Context);
            DispatchDomainEventsAsync(eventData.Context).Wait();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            MultiTenancyTracking(eventData.Context);
            SoftDeleteTracking(eventData.Context);
            await DispatchDomainEventsAsync(eventData.Context, cancellationToken);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        /// <summary>
        /// 软删除跟踪
        /// </summary>
        /// <param name="dbContext"></param>
        private static void SoftDeleteTracking(DbContext dbContext)
        {
            var deletedEntries = dbContext.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Deleted && entry.Entity is ISoftDelete);
            deletedEntries?.ToList().ForEach(entityEntry =>
            {
                entityEntry.Reload();
                entityEntry.State = EntityState.Modified;
                ((ISoftDelete)entityEntry.Entity).IsDeleted = true;
            });
        }
        /// <summary>
        /// 多租户跟踪
        /// </summary>
        /// <param name="dbContext"></param>
        private static void MultiTenancyTracking(DbContext dbContext)
        {
            var tenantedEntries = dbContext.ChangeTracker.Entries<IMultiTenant>().Where(entry => entry.State == EntityState.Added);
            var currentTenant = dbContext.GetService<ICurrentTenant>();
            tenantedEntries?.ToList().ForEach(entityEntry =>
            {
                entityEntry.Entity.TenantId ??= currentTenant.Id;
            });
        }
        /// <summary>
        /// 触发领域事件
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task DispatchDomainEventsAsync(DbContext dbContext, CancellationToken cancellationToken = default)
        {
            var domainEntities = dbContext.ChangeTracker.Entries<IDomainEvents>().Select(e => e.Entity);
            var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();
            domainEntities.ToList().ForEach(entity => entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
