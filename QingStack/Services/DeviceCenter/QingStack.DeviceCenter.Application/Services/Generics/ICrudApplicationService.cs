/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ICrudApplicationService.cs
    文件功能描述：通用增删查改应用服务接口


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211116
    修改描述：更新方法增加id参数
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Generics
{
    public interface ICrudApplicationService<TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel>
    {
        Task<TGetResponseModel> CreateAsync(TCreateRequestModel requestModel);

        Task DeleteAsync(TKey id);

        Task<TGetResponseModel> UpdateAsync(TKey id, TUpdateRequestModel requestModel);

        Task<TGetResponseModel> GetAsync(TKey id);

        Task<PagedResponseModel<TGetListResponseModel>> GetListAsync(TGetListRequestModel requestModel);
    }
}
