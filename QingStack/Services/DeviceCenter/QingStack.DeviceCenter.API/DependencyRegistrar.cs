/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：依赖注入


    创建标识：QingRain - 2021111

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.API.Extensions.Hosting;
using QingStack.DeviceCenter.API.Extensions.Tenants;

namespace QingStack.DeviceCenter.API
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddWebApiLayer(this IServiceCollection services)
        {
            //注入自定义启动项管道过滤器
            services.AddTransient<IStartupFilter, CustomStartupFilter>();
            //本地化资源
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            //添加租户中间件
            services.AddTenantMiddleware();
            return services;
        }
    }
}
