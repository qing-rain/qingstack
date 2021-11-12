/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionValueProvider.cs
    文件功能描述：权限值提供者接口

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 权限值提供者
    /// </summary>
    public interface IPermissionValueProvider
    {
        /// <summary>
        /// 主体 ID为001的用户只能对文件a.txt进行删除操作
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 判断主体是否有访问某个权限的权限
        /// </summary>
        /// <param name="principal">身份声明</param>
        /// <param name="permission"></param>
        /// <returns></returns>

        Task<PermissionGrantResult> CheckAsync(ClaimsPrincipal principal, PermissionDefinition permission);
        /// <summary>
        /// 主体是否访问多个权限的结果
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task<MultiplePermissionGrantResult> CheckAsync(ClaimsPrincipal principal, List<PermissionDefinition> permissions);
    }
}
