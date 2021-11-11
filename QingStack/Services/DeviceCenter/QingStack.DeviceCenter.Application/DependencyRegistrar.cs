/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：应用层依赖注入


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddDomainEvents();
            return services;
        }
        private static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
