/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Product.cs
    文件功能描述：产品聚合


    创建标识：QingRain - 20211108

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class Product : BaseAggregateRoot<Guid>
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
    }
}
