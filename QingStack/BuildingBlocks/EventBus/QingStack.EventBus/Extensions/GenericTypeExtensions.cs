/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：GenericTypeExtensions.cs
    文件功能描述：泛型类型输出扩展


    创建标识：QingRain - 2021103
 ----------------------------------------------------------------*/
using System;
using System.Linq;

namespace QingStack.EventBus.Extensions
{
    public static class GenericTypeExtensions
    {
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
