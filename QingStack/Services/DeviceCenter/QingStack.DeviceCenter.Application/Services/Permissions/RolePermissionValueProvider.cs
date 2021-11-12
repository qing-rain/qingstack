/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：RolePermissionValueProvider.cs
    文件功能描述：角色权限值提供者

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 角色权限值提供者
    /// </summary>
    public class RolePermissionValueProvider : IPermissionValueProvider
    {
        public const string ProviderName = "Role";

        private readonly IPermissionStore _permissionStore;

        public string Name => ProviderName;

        public RolePermissionValueProvider(IPermissionStore permissionStore)
        {
            _permissionStore = permissionStore;
        }

        public async Task<PermissionGrantResult> CheckAsync(ClaimsPrincipal principal, PermissionDefinition permission)
        {
            var roles = principal?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

            if (roles == null || !roles.Any())
            {
                return PermissionGrantResult.Undefined;
            }

            foreach (var role in roles)
            {
                if (await _permissionStore.IsGrantedAsync(permission.Name, Name!, role))
                {
                    return PermissionGrantResult.Granted;
                }
            }

            return PermissionGrantResult.Undefined;
        }

        public async Task<MultiplePermissionGrantResult> CheckAsync(ClaimsPrincipal principal, List<PermissionDefinition> permissions)
        {
            var permissionNames = permissions.Select(x => x.Name).ToList();
            var result = new MultiplePermissionGrantResult(permissionNames.ToArray());

            var roles = principal?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();

            if (roles is null || !roles.Any())
            {
                return result;
            }

            foreach (var role in roles)
            {
                var multipleResult = await _permissionStore.IsGrantedAsync(permissionNames.ToArray(), Name!, role);

                foreach (var grantResult in multipleResult.Result.Where(grantResult => result.Result.ContainsKey(grantResult.Key) && result.Result[grantResult.Key] == PermissionGrantResult.Undefined && grantResult.Value != PermissionGrantResult.Undefined))
                {
                    result.Result[grantResult.Key] = grantResult.Value;
                    permissionNames.RemoveAll(x => x == grantResult.Key);
                }

                if (result.AllGranted || result.AllProhibited)
                {
                    break;
                }
            }

            return result;
        }
    }
}
