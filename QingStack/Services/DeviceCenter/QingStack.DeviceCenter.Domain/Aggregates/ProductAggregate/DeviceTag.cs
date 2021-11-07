/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceTag.cs
    文件功能描述：设备标签实体


    创建标识：QingRain - 2021108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public class DeviceTag : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
