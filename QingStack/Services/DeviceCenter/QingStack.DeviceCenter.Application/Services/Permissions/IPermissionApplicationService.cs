/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionApplicationService.cs
    文件功能描述：权限应用服务

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Permissions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public interface IPermissionApplicationService
    {
        /// <summary>
        /// 获取主体权限
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        Task<PermissionListResponseModel> GetAsync([NotNull] string providerName, [NotNull] string providerKey);
        /// <summary>
        /// 分配主体权限
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="requestModels"></param>
        /// <returns></returns>
        Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, IEnumerable<PermissionUpdateRequestModel> requestModels);
    }
}
