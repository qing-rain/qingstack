/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：应用层依赖注入


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211111
    修改描述：注入产品应用服务

    修改标识：QingRain - 20211111
    修改描述：注入Project泛型CRUD服务、AutoMapper

    修改标识：QingRain - 20211111
    修改描述：注入FluentValidation验证器

    修改标识：QingRain - 20211111
    修改描述：添加自定义验证器错误提示扩展

    修改标识：QingRain - 20211111
    修改描述：注入权限定义提供者、权限定义管理器

    修改标识：QingRain - 20211111
    修改描述：注入分布式缓存、权限授予仓储、注入权限值定义提供者

    修改标识：QingRain - 20211112
    修改描述：注入权限应用服务
 ----------------------------------------------------------------*/
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Application.Services.Permissions;
using QingStack.DeviceCenter.Application.Services.Products;
using QingStack.DeviceCenter.Application.Services.Projects;
using System;
using System.Linq;
using System.Reflection;

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
            //注入权限相关服务
            services.AddAuthorization();
            //自定义验证器提示扩展
            ValidatorOptions.Global.LanguageManager = new Extensions.Validators.CustomLanguageManager();
            //注入验证器
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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

            //注入权限应用服务
            services.AddTransient<IPermissionApplicationService, PermissionApplicationService>();
            return services;
        }
        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            //注入分布式内存缓存、
            services.AddDistributedMemoryCache();
            //注入权限授予结果仓储
            services.AddTransient<IPermissionStore, PermissionStore>();
            //权限定义管理器
            services.AddSingleton<IPermissionDefinitionManager, PermissionDefinitionManager>();

            var exportedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.ExportedTypes).Where(t => t.IsClass);


            //注入权限定义提供者
            var permissionDefinitionProviders = exportedTypes.Where(t => t.IsAssignableTo(typeof(IPermissionDefinitionProvider)));
            permissionDefinitionProviders.ToList().ForEach(t => services.AddSingleton(typeof(IPermissionDefinitionProvider), t));

            //注入权限值提供者
            var permissionValueProviders = exportedTypes.Where(t => t.IsAssignableTo(typeof(IPermissionValueProvider)));
            permissionValueProviders.ToList().ForEach(t => services.AddTransient(typeof(IPermissionValueProvider), t));
            return services;
        }
    }
}
