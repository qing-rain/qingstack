/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DefaultConnectionStringProvider.cs
    文件功能描述：默认连接字符串提供者


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using Microsoft.Extensions.Configuration;
using QingStack.DeviceCenter.Infrastructure.Constants;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.ConnectionStrings
{
    public class DefaultConnectionStringProvider : IConnectionStringProvider
    {
        protected readonly IConfiguration _configuration;

        public DefaultConnectionStringProvider(IConfiguration configuration) => _configuration = configuration;

        public virtual Task<string> GetAsync(string? connectionStringName = null)
        {
            connectionStringName ??= DbConstants.DefaultConnectionStringName;
            return Task.FromResult(_configuration.GetConnectionString(connectionStringName));
        }
    }
}
