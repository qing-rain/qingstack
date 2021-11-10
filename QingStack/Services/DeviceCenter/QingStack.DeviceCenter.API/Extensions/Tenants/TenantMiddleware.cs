/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantMiddleware.cs
    文件功能描述：租户中间件


    创建标识：QingRain - 20211110

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
            string? tenantIdString = null;

            if (context.Request.Headers.TryGetValue(TenantConstants.TenantId, out var headerTenantIds))
            {
                tenantIdString = headerTenantIds.First();
            }

            if (context.Request.Query.TryGetValue(TenantConstants.TenantId, out var queryTenantIds))
            {
                tenantIdString = queryTenantIds.First();
            }

            if (context.Request.Cookies.TryGetValue(TenantConstants.TenantId, out var cookieTenantId))
            {
                tenantIdString = cookieTenantId;
            }

            if (context.Request.RouteValues.TryGetValue(TenantConstants.TenantId, out var routeTenantId))
            {
                tenantIdString = routeTenantId?.ToString();
            }

            tenantIdString ??= context.User.FindFirst(TenantConstants.TenantId)?.Value;

            Guid? currentTenantId = null;

            if (!string.IsNullOrWhiteSpace(tenantIdString))
            {
                currentTenantId = Guid.Parse(tenantIdString);
            }

            using (_currentTenant.Change(currentTenantId))
            {
                await next(context);
            }
        }

    }
}
