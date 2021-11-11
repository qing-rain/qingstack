/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：应用层依赖注入


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211111
    修改描述：注入产品应用服务
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Application.Services.Products;
using System;

namespace QingStack.DeviceCenter.Application
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddDomainEvents();

            //注入应用服务
            services.AddApplicationServices();
            return services;
        }
        private static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddTransient<IProductApplicationService, ProductApplicationService>();
            return services;
        }
    }
}
