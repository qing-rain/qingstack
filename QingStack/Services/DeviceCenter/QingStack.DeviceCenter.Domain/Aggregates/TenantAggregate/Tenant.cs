/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Tenant.cs
    文件功能描述：租户信息


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class Tenant
    {
        /// <summary>
        /// Null indicates the host.
        /// Not null value for a tenant.
        /// </summary>
        public Guid? TenantId { get; }
    }
}
