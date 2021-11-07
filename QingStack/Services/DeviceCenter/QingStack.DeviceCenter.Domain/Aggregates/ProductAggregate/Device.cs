/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Device.cs
    文件功能描述：设备实体（实体和聚合根之间可以用导航属性，聚合根之间没有关系不能用导航，只能用唯一键关联）


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class Device : BaseEntity<int>
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNo { get; set; } = null!;

        /// <summary>
        /// 安装地址
        /// </summary>
        public DeviceAddress Address { get; set; } = null!;

        /// <summary>
        /// 经纬度
        /// </summary>
        public GeoCoordinate Coordinate { get; set; } = null!;

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool Online { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public Guid ProductId { get; set; } = Guid.Empty;

        /// <summary>
        /// 所属产品 导航属性 
        /// </summary>
        public Product Product { get; set; } = null!;

        /// <summary>
        /// 设备标签
        /// </summary>
        public List<DeviceTag>? Tags { get; set; }
    }
}
