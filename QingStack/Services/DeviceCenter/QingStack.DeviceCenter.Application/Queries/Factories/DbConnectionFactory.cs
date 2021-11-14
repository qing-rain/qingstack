/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DbConnectionFactory.cs
    文件功能描述：基于MqSql默认Db链接工厂实现


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using MySql.Data.MySqlClient;
using QingStack.DeviceCenter.Infrastructure.ConnectionStrings;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Queries.Factories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        public DbConnectionFactory(IConnectionStringProvider connectionStringProvider) => _connectionStringProvider = connectionStringProvider;

        static DbConnectionFactory() => DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
        public async Task<IDbConnection> CreateConnection(string? nameOrConnectionString = null)
        {
            string connectionString = await _connectionStringProvider.GetAsync(nameOrConnectionString);
            DbConnection? dbConnection = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection();
            if (dbConnection is null)
            {
                throw new ArgumentException("Unable to find the requested database provider. It may not be installed.");
            }
            dbConnection.ConnectionString = connectionString;
            return dbConnection;
        }
    }
}
