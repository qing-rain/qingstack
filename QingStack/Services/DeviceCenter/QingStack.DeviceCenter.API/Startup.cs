/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Startup.cs
    文件功能描述：启动项


    创建标识：QingRain - 

    修改标识：QingRain - 20211111
    修改描述：注入本地化资源、多语言设置

 ----------------------------------------------------------------*/
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QingStack.DeviceCenter.API.Extensions.Tenants;
using QingStack.DeviceCenter.Application;
using QingStack.DeviceCenter.Domain;
using QingStack.DeviceCenter.Domain.Repositories;
using QingStack.DeviceCenter.Infrastructure;
using System;
using System.Linq;

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
            //本地化资源
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddDomainLayer();
            services.AddInfrastructureLayer(Configuration).AddApplicationLayer();
            services.AddTenantMiddleware();
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())).AddDataAnnotationsLocalization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QingStack.DeviceCenter.API", Version = "v1" });
            });
            services.AddTenantMiddleware();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //多语言中间件
            string[] supportedCultures = new[] { "zh-CN", "en-US" };
            RequestLocalizationOptions localizationOptions = new()
            {
                ApplyCurrentCultureToResponseHeaders = false
            };
            //设置默认语言文化
            localizationOptions.SetDefaultCulture(supportedCultures.First()).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            //拦截请求解析语言版本
            app.UseRequestLocalization(localizationOptions);

            app.UseTenantMiddleware();

            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                var dataSeedProviders = serviceScope.ServiceProvider.GetServices<IDataSeedProvider>();

                foreach (IDataSeedProvider dataSeedProvider in dataSeedProviders)
                {
                    dataSeedProvider.SeedAsync(serviceScope.ServiceProvider).Wait();
                }
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QingStack.DeviceCenter.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            //注入租户中间件
            app.UseTenantMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
