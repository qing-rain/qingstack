/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantConnectionStringProvider.cs
    文件功能描述：租户连接字符串提供者


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using QingStack.DeviceCenter.Infrastructure.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.ConnectionStrings
{
    public class TenantConnectionStringProvider : DefaultConnectionStringProvider
    {
        private readonly ICurrentTenant _currentTenant;

        private readonly TenantStoreOptions _tenantStoreOptions;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">注意IOptions<TenantStoreOptions>注入方式！！</param>
        /// <param name="currentTenant"></param>
        /// <param name="configuration"></param>
        public TenantConnectionStringProvider(IOptions<TenantStoreOptions> options, ICurrentTenant currentTenant, IConfiguration configuration) : base(configuration)
        {
            _currentTenant = currentTenant;
            _tenantStoreOptions = options.Value;
        }

        public override Task<string> GetAsync(string? connectionStringName = null)
        {
            connectionStringName ??= DbConstants.DefaultConnectionStringName;

            if (_currentTenant.IsAvailable)
            {
                //当前租户配置
                var tenantConfig = _tenantStoreOptions.Tenants?.SingleOrDefault(t => t.TenantId == _currentTenant.Id);
                //当前租户连接字符串
                string? connectionString = tenantConfig?.ConnectionStrings?[connectionStringName];

                if (connectionString is not null)
                {
                    return Task.FromResult(connectionString);
                }
            }

            return base.GetAsync(connectionStringName);
        }

    }
}
