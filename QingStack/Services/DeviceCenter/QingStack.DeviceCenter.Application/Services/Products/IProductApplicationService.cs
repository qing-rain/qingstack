/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IProductApplicationService.cs
    文件功能描述：项目应用服务接口


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211115
    修改描述：调整分页请求模型ProductPagedRequestModel
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Products;
using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Products
{
    public interface IProductApplicationService : ICrudApplicationService<Guid, ProductGetResponseModel, ProductPagedRequestModel, ProductGetResponseModel, ProductCreateOrUpdateRequestModel, ProductCreateOrUpdateRequestModel>
    {
        Task<Product> GetByName(string productName);
    }
}
