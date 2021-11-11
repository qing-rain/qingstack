/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    �ļ�����Startup.cs
    �ļ�����������������


    ������ʶ��QingRain - 

    �޸ı�ʶ��QingRain - 20211111
    �޸�������ע�뱾�ػ���Դ������������

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
            //���ػ���Դ
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
            //�������м��
            string[] supportedCultures = new[] { "zh-CN", "en-US" };
            RequestLocalizationOptions localizationOptions = new()
            {
                ApplyCurrentCultureToResponseHeaders = false
            };
            //����Ĭ�������Ļ�
            localizationOptions.SetDefaultCulture(supportedCultures.First()).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            //��������������԰汾
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
            //ע���⻧�м��
            app.UseTenantMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
