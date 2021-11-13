/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    �ļ�����Startup.cs
    �ļ�����������������

    ������ʶ��QingRain - 20211112

    �޸ı�ʶ��QingRain - 20211112
    �޸�������ע����֤�����ġ�IdentityServer��ܡ�Identity����ʾ��������

    �޸ı�ʶ��QingRain - 20211113
    �޸�������������Դ�ļ���������ͼ���ػ�������ע�Ȿ�ػ�

    �޸ı�ʶ��QingRain - 20211113
    �޸�������ע����Ź��ܡ���ȡ����Ͱ�������Ϣ����������֤�����������Ӱ�����http�ͻ������� ��Ϣ�����м���������ʼ����ֲ�ʽ����

    �޸ı�ʶ��QingRain - 20211113
    �޸���������ȡ֤��
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
            //������Դ�ļ�
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            //������ͼ���ػ�������ע�Ȿ�ػ�
            services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddViewLocalization().AddDataAnnotationsLocalization();
            //ע��ֲ�ʽ����
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            //ע����֤������
            services.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(Configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });
            //ע��Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                //����ǿ������
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //ע��IdentityServer
            services.AddIdentityServer().AddAspNetIdentity<ApplicationUser>()
              //.AddDeveloperSigningCredential()
              .AddSigningCredential(Certificate.Get())
              //�ͻ�����������
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  });
              })
              //��֤����ʱ����
              .AddOperationalStore(options =>
              {
                  options.ConfigureDbContext = builder => builder.UseMySql(Configuration.GetConnectionString(DbConstants.DefaultConnectionStringName), serverVersion, sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                      sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                      sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                  });
              });

            //��ȡ����������
            services.Configure<AlibabaCloudOptions>(Configuration.GetSection("AlibabaCloud"));

            //ע�밢������֤������
            services.AddTransient<AliyunAuthHandler>();

            //����http�ͻ������� ��Ϣ�����м��
            services.AddHttpClient("aliyun").AddHttpMessageHandler<AliyunAuthHandler>();


            //ע�뷢���ʼ������Ź���
            services.AddTransient<IEmailSender, AuthMessageSender>().AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //ע��������л�
            string[] supportedCultures = new[] { "zh-CN", "en-US" };
            RequestLocalizationOptions localizationOptions = new()
            {
                ApplyCurrentCultureToResponseHeaders = false
            };
            localizationOptions.SetDefaultCulture(supportedCultures.First()).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            //������ʾ����
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
            //IdentityServer�м��
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
