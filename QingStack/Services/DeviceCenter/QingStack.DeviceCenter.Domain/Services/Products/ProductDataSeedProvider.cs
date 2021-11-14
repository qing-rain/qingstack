/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductDataSeedProvider.cs
    文件功能描述：演示数据


    创建标识：QingRain - 20211110

    修改标识：QingRain - 20211114
    修改描述：增加租户信息

    修改标识：QingRain - 20211114
    修改描述：租户切换
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Services.Products
{
    public class ProductDataSeedProvider : IDataSeedProvider
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly ICurrentTenant _currentTenant;
        public ProductDataSeedProvider(IRepository<Product, Guid> productRepository, ICurrentTenant currentTenant)
        {
            _productRepository = productRepository;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (await _productRepository.GetCountAsync() <= 0)
            {
                for (int i = 1; i < 30; i++)
                {
                    Guid? tenantId = null;

                    if (i >= 10 && i < 20)
                    {
                        tenantId = Guid.Parse($"f30e402b-9de2-4b48-9ff0-c073cf499102");
                    }

                    if (i >= 20 && i < 30)
                    {
                        tenantId = Guid.Parse($"f30e402b-9de2-4b48-9ff0-c073cf499103");
                    }
                    //租户切换
                    using (_currentTenant.Change(tenantId))
                    {
                        var product = new Product { Name = $"Product{i.ToString().PadLeft(2, '0')}" };
                        await _productRepository.InsertAsync(product, true);
                    }
                }
            }
        }
    }
}
