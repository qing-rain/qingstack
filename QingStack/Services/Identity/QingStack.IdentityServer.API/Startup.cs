/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Startup.cs
    文件功能描述：启动项

    创建标识：QingRain - 20211112

    修改标识：QingRain - 20211112
    修改描述：注入认证上下文、IdentityServer框架、Identity、演示数据生成

    修改标识：QingRain - 20211113
    修改描述：配置资源文件、启用视图本地化、数据注解本地化

    修改标识：QingRain - 20211113
    修改描述：注入短信功能、读取阿里巴巴配置信息、阿里云认证处理器、增加阿里云http客户端容器 消息处理中间件、发送邮件、分布式缓存

    修改标识：QingRain - 20211113
    修改描述：读取证书
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QingStack.IdentityServer.API.Aggregates;
using QingStack.IdentityServer.API.Certificates;
using QingStack.IdentityServer.API.Constants;
using QingStack.IdentityServer.API.EntityFrameworks;
using QingStack.IdentityServer.API.Infrastructure.Aliyun;
using QingStack.IdentityServer.API.Services;
using System;
using System.Linq;
using System.Reflection;

namespace QingStack.IdentityServer.API
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
            //配置资源文件
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            //启用视图本地化、数据注解本地化
            services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddViewLocalization().AddDataAnnotationsLocalization();
            //注入分布式缓存
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            //注入认证上下文
            services.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });
            //注入Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                //密码强度设置
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //注入IdentityServer
            services.AddIdentityServer().AddAspNetIdentity<ApplicationUser>()
              //.AddDeveloperSigningCredential()
              .AddSigningCredential(Certificate.Get())
              //客户端配置数据
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  });
              })
              //认证运行时数据
              .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  });
              });

            //读取阿里云配置
            services.Configure<AlibabaCloudOptions>(Configuration.GetSection("AlibabaCloud"));

            //注入阿里云认证处理器
            services.AddTransient<AliyunAuthHandler>();

            //增加http客户端容器 消息处理中间件
            services.AddHttpClient("aliyun").AddHttpMessageHandler<AliyunAuthHandler>();


            //注入发送邮件、短信功能
            services.AddTransient<IEmailSender, AuthMessageSender>().AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //注入多语言切换
            string[] supportedCultures = new[] { "zh-CN", "en-US" };
            RequestLocalizationOptions localizationOptions = new()
            {
                ApplyCurrentCultureToResponseHeaders = false
            };
            localizationOptions.SetDefaultCulture(supportedCultures.First()).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            //生成演示数据
            SampleDataSeed.SeedAsync(app).Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //IdentityServer中间件
            app.UseIdentityServer();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
