/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IncludeRelatedPropertiesOptions.cs
    文件功能描述：导航属性配置选项


    创建标识：QingRain - 20211118

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class IncludeRelatedPropertiesOptions
    {
        private readonly IDictionary<Type, object> _includeOptions = new Dictionary<Type, object>();

        public void ConfigIncludes<TEntity>(Func<IQueryable<TEntity>, IQueryable<TEntity>> action) where TEntity : BaseEntity
        {
            _includeOptions.TryAdd(typeof(TEntity), action);
        }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> Get<TEntity>() where TEntity : BaseEntity
        {
            if (_includeOptions.TryGetValue(typeof(TEntity), out var value))
            {
                return (Func<IQueryable<TEntity>, IQueryable<TEntity>>)value;
            }

            return query => query;
        }
    }
}
