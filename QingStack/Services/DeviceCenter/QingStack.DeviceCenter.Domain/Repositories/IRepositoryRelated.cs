/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IRepository.cs
    文件功能描述：加载导航属性接口


    创建标识：QingRain - 20211118

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Repositories
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Related Navigation Property

        Task<IQueryable<TEntity>> IncludeRelatedAsync(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task LoadRelatedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>?>> propertyExpression, CancellationToken cancellationToken = default) where TProperty : class;

        Task LoadRelatedAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty?>> propertyExpression, CancellationToken cancellationToken = default) where TProperty : class;

        #endregion
    }
}
