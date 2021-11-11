/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：依赖注入


    创建标识：QingRain - 20211109

    修改标识：QingRain - 20211111
    修改描述：多租户依赖注入

 ----------------------------------------------------------------*/
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using QingStack.DeviceCenter.Domain.Services.Projects;
using System;
using System.Linq;

namespace QingStack.DeviceCenter.Domain
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddTransient<IProjectDomainService, ProjectDomainService>();

            //演示数据生成
            var dataSeedProviders = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.ExportedTypes).Where(t => t.IsAssignableTo(typeof(IDataSeedProvider)) && t.IsClass);
            dataSeedProviders.ToList().ForEach(t => services.AddTransient(typeof(IDataSeedProvider), t));

            services.AddSingleton<ICurrentTenantAccessor, CurrentTenantAccessor>();
            services.AddTransient<ICurrentTenant, CurrentTenant>();
            return services;
        }
    }
}
