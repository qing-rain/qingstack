/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionChecker.cs
    文件功能描述：权限检查器

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 权限检查器
    /// </summary>
    public interface IPermissionChecker
    {
        Task<bool> IsGrantedAsync([NotNull] string name);

        Task<bool> IsGrantedAsync([MaybeNull] ClaimsPrincipal claimsPrincipal, [NotNull] string name);

        Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names);

        Task<MultiplePermissionGrantResult> IsGrantedAsync([MaybeNull] ClaimsPrincipal claimsPrincipal, [NotNull] string[] names);
    }
}
