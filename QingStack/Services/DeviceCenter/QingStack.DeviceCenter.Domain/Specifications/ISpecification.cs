/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ISpecification.cs
    文件功能描述：规约接口


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QingStack.DeviceCenter.Domain.Specifications
{
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        /// <summary>
        /// select表达式
        /// </summary>
        Expression<Func<T, TResult>>? Selector { get; }
    }

    public interface ISpecification<T>
    {
        /// <summary>
        /// where条件组合
        /// </summary>
        IEnumerable<Expression<Func<T, bool>>> WhereExpressions { get; }
        /// <summary>
        /// 排序条件组合
        /// </summary>
        IEnumerable<(Expression<Func<T, object>> KeySelector, OrderTypeEnum OrderType)> OrderExpressions { get; }

        IEnumerable<IIncludeAggregator> IncludeAggregators { get; }

        IEnumerable<string> IncludeStrings { get; }
        /// <summary>
        /// 搜索条件
        /// </summary>
        IEnumerable<(Expression<Func<T, string>> selector, string searchTerm, int searchGroup)> SearchCriterias { get; }

        int? Take { get; }

        int? Skip { get; }
        /// <summary>
        /// 是否缓存
        /// </summary>
        bool CacheEnabled { get; }
        /// <summary>
        /// 缓存键
        /// </summary>
        string? CacheKey { get; }
    }
}