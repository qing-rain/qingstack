/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DependencyRegistrar.cs
    文件功能描述：依赖注入


    创建标识：QingRain - 20211109

 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using QingStack.DeviceCenter.Infrastructure.Constants;
using QingStack.DeviceCenter.Infrastructure.EntityFrameworks;
using QingStack.DeviceCenter.Infrastructure.Repositories.Products;
using System;
using System.Reflection;

namespace QingStack.DeviceCenter.Infrastructure
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
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
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddPooledDbContextFactory<DeviceCenterDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });

                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddTransient(typeof(IRepository<>), typeof(DeviceCenterEfCoreRepository<>));
            services.AddTransient(typeof(IRepository<,>), typeof(DeviceCenterEfCoreRepository<,>));

            //注入产品自定义仓储
            services.AddTransient<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
