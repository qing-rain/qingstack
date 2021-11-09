/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SpecificationEvaluator.cs
    文件功能描述：EFCore规约计算器


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using QingStack.DeviceCenter.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Specifications
{
    public class SpecificationEvaluator<T> : ISpecificationEvaluator<T> where T : class
    {
        public virtual IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification)
        {
            var query = GetQuery(inputQuery, (ISpecification<T>)specification);

            // Apply selector
            var selectQuery = query.Select(specification.Selector!);

            return selectQuery;
        }

        public virtual IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            foreach (var includeString in specification.IncludeStrings)
            {
                query = query.Include(includeString);
            }

            foreach (var includeAggregator in specification.IncludeAggregators)
            {
                var includeString = includeAggregator.IncludeString;
                if (!string.IsNullOrEmpty(includeString))
                {
                    query = query.Include(includeString);
                }
            }

            foreach (var criteria in specification.WhereExpressions)
            {
                query = query.Where(criteria);
            }

            foreach (var searchCriteria in specification.SearchCriterias.GroupBy(x => x.searchGroup))
            {
                var criterias = searchCriteria.Select(x => (x.selector, x.searchTerm));
                query = query.Search(criterias);
            }

            // Need to check for null if <Nullable> is enabled.
            if (specification.OrderExpressions != null && specification.OrderExpressions.Any())
            {
                IOrderedQueryable<T>? orderedQuery = null;

                foreach (var (KeySelector, OrderType) in specification.OrderExpressions)
                {
                    switch (OrderType)
                    {
                        case OrderTypeEnum.OrderBy:
                            orderedQuery = query.OrderBy(KeySelector);
                            break;
                        case OrderTypeEnum.OrderByDescending:
                            orderedQuery = query.OrderByDescending(KeySelector);
                            break;
                        case OrderTypeEnum.ThenBy:
                            orderedQuery = orderedQuery!.ThenBy(KeySelector);
                            break;
                        case OrderTypeEnum.ThenByDescending:
                            orderedQuery = orderedQuery!.ThenByDescending(KeySelector);
                            break;
                    }

                    if (orderedQuery != null)
                    {
                        query = orderedQuery;
                    }
                }
            }

            // If skip is 0, avoid adding to the IQueryable. It will generate more optimized SQL that way.
            if (specification.Skip != null && specification.Skip != 0)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take != null)
            {
                query = query.Take(specification.Take.Value);
            }

            return query;
        }
    }
}
