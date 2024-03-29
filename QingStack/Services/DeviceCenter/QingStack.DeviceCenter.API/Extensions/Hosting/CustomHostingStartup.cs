﻿/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomHostingStartup.cs
    文件功能描述：自定义管道


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211113
    修改描述：注入JWT认证

    修改标识：QingRain - 20211114
    修改描述：自动绑定TenantStoreOptions配置

    修改标识：QingRain - 20211115
    修改描述：添加排序解释器模型绑定提供者
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Application;
using QingStack.DeviceCenter.Domain;
using QingStack.DeviceCenter.Infrastructure;
using QingStack.DeviceCenter.Infrastructure.ConnectionStrings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
//标记HostingStartup
[assembly: HostingStartup(typeof(QingStack.DeviceCenter.API.Extensions.Hosting.CustomHostingStartup))]
namespace QingStack.DeviceCenter.API.Extensions.Hosting
{
    public class CustomHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                //依赖注入领域层、基础设施层、应用层、表现层
                services.AddDomainLayer().AddInfrastructureLayer(configuration).AddApplicationLayer().AddWebApiLayer();
                //用户名、角色映射
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add(nameof(ClaimTypes.Name).ToLower(), ClaimTypes.Name);

                //设置认证方式
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    options.Authority = configuration.GetValue<string>("IdentityServer:AuthorizationUrl");
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.NameClaimType = ClaimTypes.Name;
                });
                //多租户自动绑定配置
                services.Configure<TenantStoreOptions>(configuration);

                //注入模型绑定提供者
                services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
                {
                    options.ModelBinderProviders.Add(new ModelBinding.SortingBinderProvider());
                });
            });
        }
    }
}
