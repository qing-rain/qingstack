/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductRepository.cs
    文件功能描述：自定义扩展仓储


    创建标识：QingRain - 2021110
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using QingStack.DeviceCenter.Infrastructure.EntityFrameworks;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.Repositories.Products
{
    public class ProductRepository : EfCoreRepository<DeviceCenterDbContext, Product>, IProductRepository
    {
        public ProductRepository(DeviceCenterDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product> AddAsync(Product product)
        {
            await DbSet.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
