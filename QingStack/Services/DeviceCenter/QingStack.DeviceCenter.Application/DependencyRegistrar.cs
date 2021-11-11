/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：应用层依赖注入


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211111
    修改描述：注入产品应用服务

    修改标识：QingRain - 20211111
    修改描述：注入Project泛型CRUD服务、AutoMapper
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Application.Services.Products;
using QingStack.DeviceCenter.Application.Services.Projects;
using System;

namespace QingStack.DeviceCenter.Application
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            //注入领域事件
            services.AddDomainEvents();
            //注入模型映射
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
            //注入Project泛型CRUD服务
            services.AddTransient(typeof(ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel>), typeof(ProjectApplicationService));
            services.AddTransient<IProductApplicationService, ProductApplicationService>();
            return services;
        }
    }
}
