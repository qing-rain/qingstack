﻿/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：QueryableOrderByExtensions.cs
    文件功能描述：排序扩展方法


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211115
    修改描述：扩展排序方法
 ----------------------------------------------------------------*/
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class QueryableOrderByExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName) => OrderingHelper<T>.OrderBy(source, propertyName);

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName) => OrderingHelper<T>.OrderByDescending(source, propertyName);

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName) => OrderingHelper<T>.ThenBy(source, propertyName);

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName) => OrderingHelper<T>.ThenByDescending(source, propertyName);

        private static class OrderingHelper<TSource>
        {
            private static readonly ConcurrentDictionary<string, LambdaExpression> cached = new();

            public static IOrderedQueryable<TSource> OrderBy(IQueryable<TSource> source, string propertyName) => Queryable.OrderBy(source, (dynamic)CreateLambdaExpression(propertyName));

            public static IOrderedQueryable<TSource> OrderByDescending(IQueryable<TSource> source, string propertyName) => Queryable.OrderByDescending(source, (dynamic)CreateLambdaExpression(propertyName));

            public static IOrderedQueryable<TSource> ThenBy(IOrderedQueryable<TSource> source, string propertyName) => Queryable.ThenBy(source, (dynamic)CreateLambdaExpression(propertyName));

            public static IOrderedQueryable<TSource> ThenByDescending(IOrderedQueryable<TSource> source, string propertyName) => Queryable.ThenByDescending(source, (dynamic)CreateLambdaExpression(propertyName));

            private static LambdaExpression CreateLambdaExpression(string propertyName)
            {
                if (cached.ContainsKey(propertyName))
                {
                    return cached[propertyName];
                }
                var parameter = Expression.Parameter(typeof(TSource));
                var body = Expression.Property(parameter, propertyName);
                var keySelector = Expression.Lambda(body, parameter);
                cached[propertyName] = keySelector;

                return keySelector;
            }
        }
    }
}

