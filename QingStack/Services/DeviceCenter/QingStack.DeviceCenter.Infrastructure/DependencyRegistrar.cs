﻿/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：依赖注入


    创建标识：QingRain - 20211109

    修改标识：QingRain - 20211111
    修改描述：注入权限授予自定义仓储

    修改标识：QingRain - 20211114
    修改描述：注入自定义保存拦截器

    修改标识：QingRain - 20211114
    修改描述：注入租户连接字符串服务、连接字符串提供者

    修改标识：QingRain - 20211114
    修改描述：注入幂等性模型管理器服务

    修改标识：QingRain - 20211118
    修改描述：增加产品导航属性配置
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using QingStack.DeviceCenter.Infrastructure.ConnectionStrings;
using QingStack.DeviceCenter.Infrastructure.Constants;
using QingStack.DeviceCenter.Infrastructure.EntityFrameworks;
using QingStack.DeviceCenter.Infrastructure.Idempotency;
using QingStack.DeviceCenter.Infrastructure.Repositories.Permissions;
using QingStack.DeviceCenter.Infrastructure.Repositories.Products;
using System;
using System.Reflection;

namespace QingStack.DeviceCenter.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //注入租户连接字符串服务
            services.AddTransient<IConnectionStringProvider, TenantConnectionStringProvider>();
            services.AddEntityFrameworkMySql();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            //DbContext池 提高效率
            services.AddDbContextPool<DeviceCenterDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
                //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
                //optionsBuilder.EnableSensitiveDataLogging();
                //optionsBuilder.EnableDetailedErrors();

                IMediator mediator = serviceProvider.GetService<IMediator>() ?? new NullMediator();
                optionsBuilder.AddInterceptors(new CustomSaveChangesInterceptor(mediator));

                //连接字符串提供者
                var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
                optionsBuilder.AddInterceptors(new TenantDbConnectionInterceptor(connectionStringProvider));

                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddPooledDbContextFactory<DeviceCenterDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
                IMediator mediator = serviceProvider.GetService<IMediator>() ?? new NullMediator();
                optionsBuilder.AddInterceptors(new CustomSaveChangesInterceptor(mediator));

                //连接字符串提供者
                var connectionStringProvider = serviceProvider.GetRequiredService<IConnectionStringProvider>();
                optionsBuilder.AddInterceptors(new TenantDbConnectionInterceptor(connectionStringProvider));

                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddTransient(typeof(IRepository<>), typeof(DeviceCenterEfCoreRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(DeviceCenterEfCoreRepository<,>));

            //注入产品自定义仓储
            services.AddTransient<IProductRepository, ProductRepository>();

            //注入权限授予自定义仓储
            services.AddTransient<IPermissionGrantRepository, PermissionGrantRepository>();

            services.AddTransient<IRequestManager, RequestManager>();

            //增加产品导航属性配置
            services.Configure<IncludeRelatedPropertiesOptions>(options =>
            {
                options.ConfigIncludes<Product>(e => e.Include(e => e.Devices).ThenInclude(e => e.Address));
            });
            return services;
        }
    }
}
