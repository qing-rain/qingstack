/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：GeoCoordinate.cs
    文件功能描述：经纬度值对象


    创建标识：QingRain - 20211108

    修改标识：QingRain - 20211109
    修改描述：增加析构元组
 ----------------------------------------------------------------*/
using System;
using System.Linq;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    /// <summary>
    /// 经纬度
    /// </summary>
    public record GeoCoordinate
    {
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
        /// <summary>
        /// 转换经纬度显示X,Y
        /// </summary>
        /// <returns></returns>

        public override string ToString() => $"{Latitude},{Longitude}";

        /// <summary>
        /// 隐式转换经纬度实体为字符串
        /// </summary>
        /// <param name="geo"></param>

        public static implicit operator string(GeoCoordinate geo) => geo.ToString();
        /// <summary>
        /// 析构元组
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public void Deconstruct(out double? latitude, out double? longitude)
        {
            latitude = Latitude;
            longitude = Longitude;
        }
        /// <summary>
        /// 字符串显式转换为经纬度实体
        /// </summary>
        /// <param name="str"></param>

        public static explicit operator GeoCoordinate(string str)
        {
            GeoCoordinate geoCoordinate = new();

            if (!string.IsNullOrWhiteSpace(str))
            {
                double[] arr = Array.ConvertAll(str.Split(','), i => Convert.ToDouble(i));

                geoCoordinate.Latitude = arr.First();
                geoCoordinate.Longitude = arr.Last();
            }

            return geoCoordinate;
        }
    }
}
