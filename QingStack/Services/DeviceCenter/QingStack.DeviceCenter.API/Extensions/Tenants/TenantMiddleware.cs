/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantMiddleware.cs
    文件功能描述：租户中间件


    创建标识：QingRain - 20211110

    修改标识：QingRain - 20211114
    修改描述：

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Http;
using QingStack.DeviceCenter.API.Constants;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Extensions.Tenants
{
    public class TenantMiddleware : IMiddleware
    {
        private readonly ICurrentTenant _currentTenant;

        public TenantMiddleware(ICurrentTenant currentTenant) => _currentTenant = currentTenant;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string? tenantIdString = ResolveTenantId(context);
            if (Guid.TryParse(tenantIdString, out var parsedTenantId))
            {
                //切换租户
                using (_currentTenant.Change(parsedTenantId))
                {
                    await next(context);
                }
            }
            else
            {
                await next(context);
            }
        }
        protected virtual string? ResolveTenantId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(TenantClaimTypes.TenantId, out var headerValues))
            {
                return headerValues.First();
            }

            if (httpContext.Request.Query.TryGetValue(TenantClaimTypes.TenantId, out var queryValues))
            {
                return queryValues.First();
            }

            if (httpContext.Request.Cookies.TryGetValue(TenantClaimTypes.TenantId, out var cookieValue))
            {
                return cookieValue;
            }

            if (httpContext.Request.RouteValues.TryGetValue(TenantClaimTypes.TenantId, out var routeValue))
            {
                return routeValue?.ToString();
            }

            return httpContext.User.FindFirst(TenantClaimTypes.TenantId)?.Value;
        }
    }
}
