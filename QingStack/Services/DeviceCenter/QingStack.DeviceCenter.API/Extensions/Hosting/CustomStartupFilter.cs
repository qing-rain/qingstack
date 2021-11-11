/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomStartupFilter.cs
    文件功能描述：自定义启动项管道过滤器


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using QingStack.DeviceCenter.API.Extensions.Tenants;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace QingStack.DeviceCenter.API.Extensions.Hosting
{
    public class CustomStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
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

                //注入多租户中间件拦截
                app.UseTenantMiddleware();

                //解析验证器模型名称
                IStringLocalizerFactory? localizerFactory = app.ApplicationServices.GetService<IStringLocalizerFactory>();

                FluentValidation.ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
                {
                    string? displayName = string.Empty;

                    DisplayAttribute? displayColumnAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();

                    if (displayColumnAttribute is not null)
                    {
                        displayName = displayColumnAttribute.Name;
                    }

                    DisplayNameAttribute? displayNameAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayNameAttribute>().FirstOrDefault();

                    if (displayNameAttribute is not null)
                    {
                        displayName = displayNameAttribute.DisplayName;
                    }

                    if (!string.IsNullOrWhiteSpace(displayName) && localizerFactory is not null)
                    {
                        return localizerFactory.Create(type)[displayName];
                    }

                    if (!string.IsNullOrWhiteSpace(displayName))
                    {
                        return displayName;
                    }

                    return memberInfo.Name;
                };
                using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
                {
                    var dataSeedProviders = serviceScope.ServiceProvider.GetServices<IDataSeedProvider>();

                    foreach (IDataSeedProvider dataSeedProvider in dataSeedProviders)
                    {
                        dataSeedProvider.SeedAsync(serviceScope.ServiceProvider).Wait();
                    }
                }
                next(app);
            };
        }
    }
}
