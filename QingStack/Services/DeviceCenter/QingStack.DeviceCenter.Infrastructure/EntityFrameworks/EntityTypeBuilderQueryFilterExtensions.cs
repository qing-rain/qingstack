/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：EntityTypeBuilderQueryFilterExtensions.cs
    文件功能描述：实体类过滤扩展方法


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public static class EntityTypeBuilderQueryFilterExtensions
    {
        /// <summary>
        /// Support multiple HasQueryFilter calls on same entity type
        /// https://github.com/dotnet/efcore/issues/10275
        /// </summary>
        internal static void AddQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder, Expression<Func<T, bool>> expression)
        {
            var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), parameterType, expression.Body);

            var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
            if (currentQueryFilter is not null)
            {
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
        }
    }
}
