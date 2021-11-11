/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IProductApplicationService.cs
    文件功能描述：项目应用服务接口


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Products;
using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Products
{
    public interface IProductApplicationService : ICrudApplicationService<Guid, ProductGetResponseModel, PagedRequestModel, ProductGetResponseModel, ProductCreateOrUpdateRequestModel, ProductCreateOrUpdateRequestModel>
    {
        Task<Product> GetByName(string productName);
    }
}
