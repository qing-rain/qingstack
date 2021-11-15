/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Product.cs
    文件功能描述：产品聚合


    创建标识：QingRain - 20211108

    修改标识：QingRain - 20211114
    修改描述：实现软删除、多租户接口

    修改标识：QingRain - 20211116
    修改描述：增加创建日期属性
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class Product : BaseAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 所属项目
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// 设备列表
        /// </summary>
        public List<Device>? Devices { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string? Remark { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public Guid? TenantId { get; set; }
    }
}
