/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IDbConnectionFactory.cs
    文件功能描述：Db链接创建工厂


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using System.Data;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Queries.Factories
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnection(string? nameOrConnectionString = null);
    }
}
