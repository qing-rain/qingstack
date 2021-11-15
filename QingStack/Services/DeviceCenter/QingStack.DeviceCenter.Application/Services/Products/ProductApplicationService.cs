/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductApplicationService.cs
    文件功能描述：项目应用服务


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211111
    修改描述：增加自定义扩展方法

    修改标识：QingRain - 20211115
    修改描述：重写过滤器、调整分页请求模型ProductPagedRequestModel
 ----------------------------------------------------------------*/
using AutoMapper;
using QingStack.DeviceCenter.Application.Models.Products;
using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Products
{
    public class ProductApplicationService : CrudApplicationService<Product, Guid, ProductGetResponseModel, ProductPagedRequestModel, ProductGetResponseModel, ProductCreateOrUpdateRequestModel, ProductCreateOrUpdateRequestModel>, IProductApplicationService
    {
        public ProductApplicationService(IRepository<Product, Guid> repository, IMapper mapper) : base(repository, mapper)
        {

        }
        protected override IQueryable<Product> CreateFilteredQuery(ProductPagedRequestModel requestModel)
        {
            if (requestModel.Keyword is not null && !string.IsNullOrWhiteSpace(requestModel.Keyword))
            {
                return Repository.Query.Where(e => e.Name.Contains(requestModel.Keyword));
            }

            return base.CreateFilteredQuery(requestModel);
        }
        public async Task<Product> GetByName(string productName)
        {
            return await Repository.GetAsync(p => p.Name == productName);
        }
    }
}
