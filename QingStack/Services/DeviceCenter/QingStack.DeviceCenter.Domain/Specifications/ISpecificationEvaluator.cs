/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ISpecificationEvaluator.cs
    文件功能描述：规约计算器


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using System.Linq;

namespace QingStack.DeviceCenter.Domain.Specifications
{
    public interface ISpecificationEvaluator<T> where T : class
    {
        IQueryable<TResult> GetQuery<TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification);

        IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification);
    }
}