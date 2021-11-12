/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGrantRepository.cs
    文件功能描述：权限授予仓储

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;
using QingStack.DeviceCenter.Infrastructure.EntityFrameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Repositories.Permissions
{
    public class PermissionGrantRepository : EfCoreRepository<DeviceCenterDbContext, PermissionGrant, Guid>, IPermissionGrantRepository
    {
        public PermissionGrantRepository(DeviceCenterDbContext dbContext) : base(dbContext) { }

        public async Task<PermissionGrant> FindAsync(string name, string providerName, string providerKey, CancellationToken cancellationToken = default)
        {
            return await DbSet.OrderBy(x => x.Id).FirstOrDefaultAsync(s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey, cancellationToken);
        }

        public async Task<List<PermissionGrant>> GetListAsync(string providerName, string providerKey, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(s => s.ProviderName == providerName && s.ProviderKey == providerKey).ToListAsync(cancellationToken);
        }

        public async Task<List<PermissionGrant>> GetListAsync(string[] names, string providerName, string providerKey, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(s => names.Contains(s.Name) && s.ProviderName == providerName && s.ProviderKey == providerKey).ToListAsync(cancellationToken);
        }
    }
}
