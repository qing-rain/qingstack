/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IMultiTenant.cs
    文件功能描述：多租户


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Domain.Entities
{
    /// <summary>
    /// 多租户
    /// </summary>
    public interface IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        Guid? TenantId { get; set; }
    }
}
