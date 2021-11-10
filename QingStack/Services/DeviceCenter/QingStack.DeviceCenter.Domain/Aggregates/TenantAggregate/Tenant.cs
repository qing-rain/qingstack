/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Tenant.cs
    文件功能描述：租户信息


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class Tenant : BaseAggregateRoot<Guid>
    {
        public string Name { get; set; } = null!;

        public List<TenantConnectionString> ConnectionStrings { get; protected set; } = new List<TenantConnectionString>();
    }

}
