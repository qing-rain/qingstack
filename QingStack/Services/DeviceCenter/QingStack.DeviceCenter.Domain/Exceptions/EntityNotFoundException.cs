/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：EntityNotFoundException.cs
    文件功能描述：实体未找到异常


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; set; }

        public EntityNotFoundException(Type entityType)
        {
            EntityType = entityType;
        }

        public override string ToString()
        {
            return $"There is no such an entity given given id. Entity type: {EntityType.FullName}";
        }
    }
}
