/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Tenant.cs
    文件功能描述：租户连接字符串信息


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class TenantConnectionString : BaseEntity
    {
        public virtual Guid TenantId { get; set; }

        public virtual string Name { get; set; } = null!;

        public virtual string Value { get; set; } = null!;

        public override object[] GetKeys()
        {
            return new object[] { TenantId, Name };
        }
    }
}
