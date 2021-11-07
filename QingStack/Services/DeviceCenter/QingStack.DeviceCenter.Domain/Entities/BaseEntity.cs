/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：BaseEntity.cs
    文件功能描述：基本实体模型


    创建标识：QingRain - 2021107
 ----------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Domain.Entities
{
    /// <summary>
    /// 联合主键标识实体
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 联合主键
        /// </summary>
        /// <returns></returns>
        public abstract object[] GetKeys();
    }
    /// <summary>
    /// 唯一主键标识实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseEntity<Tkey> : BaseEntity
    {
        [AllowNull]
        /// <summary>
        /// 唯一主键
        /// </summary>
        public Tkey Id { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { Id! };
        }
    }
}
