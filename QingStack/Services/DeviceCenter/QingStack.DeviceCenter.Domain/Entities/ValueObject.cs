/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ValueObject.cs
    文件功能描述：值对象


    创建标识：QingRain - 2021108
 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace QingStack.DeviceCenter.Domain.Entities
{
    public abstract class ValueObject
    {
        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }
            return left is null || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }
        /// <summary>
        /// 比较器
        /// </summary>
        /// <returns></returns>

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Select(x => x is not null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
        }
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>

        public ValueObject? GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }
    }
}
