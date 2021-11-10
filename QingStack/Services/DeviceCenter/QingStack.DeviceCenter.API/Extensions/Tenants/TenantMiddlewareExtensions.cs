/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantMiddlewareExtensions.cs
    文件功能描述：租户中间件扩展方法


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace QingStack.DeviceCenter.API.Extensions.Tenants
{
    public static class TenantMiddlewareExtensions
    {
        public static IServiceCollection AddTenantMiddleware(this IServiceCollection services)
        {
            return services.AddTransient<TenantMiddleware>();
        }

        public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
