/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantDbConnectionInterceptor.cs
    文件功能描述：租户Db连接拦截器


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.ConnectionStrings
{
    public class TenantDbConnectionInterceptor : DbConnectionInterceptor
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public TenantDbConnectionInterceptor(IConnectionStringProvider connectionStringProvider) => _connectionStringProvider = connectionStringProvider;

        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            connection.ConnectionString = _connectionStringProvider.GetAsync().Result;
            return base.ConnectionOpening(connection, eventData, result);
        }

        public override async ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            connection.ConnectionString = await _connectionStringProvider.GetAsync();
            return await base.ConnectionOpeningAsync(connection, eventData, result, cancellationToken);
        }
    }
}
