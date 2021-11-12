/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：NullPermissionStore.cs
    文件功能描述：空模式，空存储

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 空模式，空存储
    /// </summary>
    public class NullPermissionStore : IPermissionStore
    {
        public Task<bool> IsGrantedAsync([NotNull] string name, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            return Task.FromResult(true);
        }

        public Task<MultiplePermissionGrantResult> IsGrantedAsync([NotNull] string[] names, [MaybeNull] string providerName, [MaybeNull] string providerKey)
        {
            return Task.FromResult(new MultiplePermissionGrantResult(names, PermissionGrantResult.Prohibited));
        }
    }
}
