/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    �ļ�����Startup.cs
    �ļ�����������������


    ������ʶ��QingRain - 

    �޸ı�ʶ��QingRain - 20211111
    �޸�������ע�뱾�ػ���Դ������������

    �޸ı�ʶ��QingRain - 20211111
    �޸�������������֤��ģ������

    �޸ı�ʶ��QingRain - 20211113
    �޸�����������Swagger����

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QingStack.DeviceCenter.API.Extensions.Tenants;
using QingStack.DeviceCenter.API.Infrastructure.Swagger;
using System;
using System.Collections.Generic;

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
                //ע�����������
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                //�����������Ȩ��¼
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
                                //����Ȩ�޷�Χ
                                { "openid", "Your user identifier" },
                                { "devicecenter", "Device Center API" }
                            }
                        }
                    }
                });
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
                //���ü��ؽ���
                c.IndexStream = () => GetType().Assembly.GetManifestResourceStream($"{GetType().Assembly.GetName().Name}.Infrastructure.Swagger.Index.html");


                c.OAuthClientId("devicecenterswagger");
                c.OAuthClientSecret("secret");
                c.OAuthAppName("Device Center Swagger");
                c.OAuthUsePkce();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            //��֤����Ȩ
            app.UseAuthentication().UseAuthorization();

            //ע����⻧�м������
            app.UseTenantMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
