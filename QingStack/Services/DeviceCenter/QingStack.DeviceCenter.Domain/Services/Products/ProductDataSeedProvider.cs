/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductDataSeedProvider.cs
    文件功能描述：演示数据


    创建标识：QingRain - 2021110

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Services.Products
{
    public class ProductDataSeedProvider : IDataSeedProvider
    {
        private readonly IRepository<Product, Guid> _productRepository;
        public ProductDataSeedProvider(IRepository<Product, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (await _productRepository.GetCountAsync() <= 0)
            {
                for (int i = 1; i < 30; i++)
                {
                    var product = new Product { Name = $"Product{i.ToString().PadLeft(2, '0')}" };
                    await _productRepository.InsertAsync(product, true);
                }
            }
        }
    }
}
