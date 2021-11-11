/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ICrudApplicationService.cs
    文件功能描述：通用增删查改应用服务接口


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Generics
{
    public interface ICrudApplicationService<TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel>
    {
        Task<TGetResponseModel> CreateAsync(TCreateRequestModel requestModel);

        Task DeleteAsync(TKey id);

        Task<TGetResponseModel> UpdateAsync(TUpdateRequestModel requestModel);

        Task<TGetResponseModel> GetAsync(TKey id);

        Task<PagedResponseModel<TGetListResponseModel>> GetListAsync(TGetListRequestModel requestModel);
    }
}
