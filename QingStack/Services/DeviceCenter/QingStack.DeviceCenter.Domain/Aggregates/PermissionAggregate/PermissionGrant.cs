/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGrant.cs
    文件功能描述：权限授予聚合根 

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;

namespace QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate
{
    /// <summary>
    /// 权限批准 结构：例如ID为001的用户只能对文件a.txt进行删除操作
    /// </summary>
    public class PermissionGrant : BaseAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 主体类型 用户、部门、角色等
        /// </summary>
        public string ProviderName { get; set; } = null!;
        /// <summary>
        /// 主体唯一标识
        /// </summary>
        public string ProviderKey { get; set; } = null!;
    }
}
