/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomHostingStartup.cs
    文件功能描述：自定义管道


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QingStack.DeviceCenter.Application;
using QingStack.DeviceCenter.Domain;
using QingStack.DeviceCenter.Infrastructure;
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
            });
        }
    }
}
