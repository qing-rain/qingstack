/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Startup.cs
    文件功能描述：启动项


    创建标识：QingRain - 

    修改标识：QingRain - 20211111
    修改描述：注入本地化资源、多语言设置

    修改标识：QingRain - 20211111
    修改描述：解析验证器模型名称

    修改标识：QingRain - 20211113
    修改描述：调整Swagger配置

    修改标识：QingRain - 20211115
    修改描述：调整Swagger支持不可空引用类型、注入参数转换过滤器

    修改标识：QingRain - 20211115
    修改描述：Swagger配置排序解释器输入方式

    修改标识：QingRain - 20211212
    修改描述：调整租户中间件拦截执行顺序

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QingStack.DeviceCenter.API.Extensions.Tenants;
using QingStack.DeviceCenter.API.Infrastructure.Swagger;
using QingStack.DeviceCenter.Application.Services.Generics;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace QingStack.DeviceCenter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddCustomExtensions();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Device Center API", Version = "v1" });
                //支持不可空引用类型
                c.SupportNonNullableReferenceTypes();
                c.UseAllOfToExtendReferenceSchemas();

                //注入操作过滤器
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //注入参数转换过滤器
                c.OperationFilter<CamelCaseNamingOperationFilter>();
                c.CustomOperationIds(api => $"{JsonNamingPolicy.CamelCase.ConvertName(api.ActionDescriptor.RouteValues["action"])}");
                //点击锁进行授权登录
                string identityServer = Configuration.GetValue<string>("IdentityServer:AuthorizationUrl");

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{identityServer}/connect/authorize"),
                            TokenUrl = new Uri($"{identityServer}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                //设置权限范围
                                { "openid", "Your user identifier" },
                                { "devicecenter", "Device Center API" }
                            }
                        }
                    }
                });

                //配置排序解释器输入方式
                c.MapType<IEnumerable<SortingDescriptor>>(() => new OpenApiSchema { Type = "string", Format = "json" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device Center API v1");
                c.DocumentTitle = "Device Center API Document";
                //设置加载界面
                c.IndexStream = () => GetType().Assembly.GetManifestResourceStream($"{GetType().Assembly.GetName().Name}.Infrastructure.Swagger.Index.html");


                c.OAuthClientId("devicecenterswagger");
                c.OAuthClientSecret("secret");
                c.OAuthAppName("Device Center Swagger");
                c.OAuthUsePkce();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            //认证、授权
            app.UseAuthentication();

            //注入多租户中间件拦截
            app.UseTenantMiddleware();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
