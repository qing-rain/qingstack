/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IProductRepository.cs
    文件功能描述：自定义扩展仓储


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Repositories;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> AddAsync(Product product);
    }
}
