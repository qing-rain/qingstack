/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceAddress.cs
    文件功能描述：设备地址值对象


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    /// <summary>
    /// 设备地址
    /// </summary>
    public class DeviceAddress : ValueObject
    {
        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; } = null!;
        /// <summary>
        /// 城市 地址确定后不可更改
        /// </summary>
        public string City { get; private set; } = null!;
        /// <summary>
        /// 状态
        /// </summary>

        public string State { get; private set; } = null!;
        /// <summary>
        /// 国家
        /// </summary>

        public string Country { get; private set; } = null!;
        /// <summary>
        /// 邮政编码
        /// </summary>

        public string ZipCode { get; private set; } = null!;

        public DeviceAddress() { }

        public DeviceAddress(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
