/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ICurrentTenantAccessor.cs
    文件功能描述：当前租户访问器


    创建标识：QingRain - 20211110


 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public interface ICurrentTenantAccessor
    {
        Guid? TenantId { get; set; }
    }
}
