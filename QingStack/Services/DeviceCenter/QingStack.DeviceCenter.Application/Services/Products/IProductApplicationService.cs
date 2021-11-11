/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IProductApplicationService.cs
    文件功能描述：项目应用服务接口


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Products;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Products
{
    public interface IProductApplicationService
    {
        Task<ProductGetResponseModel> CreateAsync(ProductCreateOrUpdateRequestModel requestModel);

        Task DeleteAsync(Guid id);

        Task<ProductGetResponseModel> UpdateAsync(ProductCreateOrUpdateRequestModel requestModel);

        Task<ProductGetResponseModel> GetAsync(Guid id);

        Task<PagedResponseModel<ProductGetResponseModel>> GetListAsync(PagedRequestModel requestModel);
    }
}
