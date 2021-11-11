/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Startup.cs
    文件功能描述：启动项


    创建标识：QingRain - 

    修改标识：QingRain - 20211111
    修改描述：注入本地化资源、多语言设置

    修改标识：QingRain - 20211111
    修改描述：解析验证器模型名称

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Device Center API v1");
                    c.DocumentTitle = "Device Center API Document";
                    //设置加载界面
                    c.IndexStream = () => GetType().Assembly.GetManifestResourceStream($"{GetType().Assembly.GetName().Name}.Infrastructure.Swagger.Index.html");
                });
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
