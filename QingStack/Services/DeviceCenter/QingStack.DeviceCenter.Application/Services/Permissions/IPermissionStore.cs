/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionStore.cs
    文件功能描述：自定义权限存储接口，隔离仓储对权限设计的影响

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 权限存储接口，隔离仓储对权限设计的影响
    /// </summary>
    public interface IPermissionStore
    {
        /// <summary>
        /// 是否授权
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        Task<bool> IsGrantedAsync([NotNull] string name, [MaybeNull] string providerName, [MaybeNull] string providerKey);
        /// <summary>
        /// 通过权限名返回多权限授权结果
        /// </summary>
        /// <param name="names"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names, [MaybeNull] string providerName, [MaybeNull] string providerKey);
    }
}
