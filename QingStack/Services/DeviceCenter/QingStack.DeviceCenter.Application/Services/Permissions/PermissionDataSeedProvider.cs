/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionDataSeedProvider.cs
    文件功能描述：权限数据生成器

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Permissions;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public class PermissionDataSeedProvider : IDataSeedProvider
    {
        private readonly IPermissionApplicationService _permissionService;

        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public PermissionDataSeedProvider(IPermissionApplicationService permissionService, IPermissionDefinitionManager permissionDefinitionManager)
        {
            _permissionService = permissionService;
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var permissionNames = _permissionDefinitionManager.GetPermissions().Where(p => !p.AllowedProviders.Any() || p.AllowedProviders.Contains(RolePermissionValueProvider.ProviderName)).Select(p => p.Name).ToArray();
            var permissionModels = Array.ConvertAll(permissionNames, pn => new PermissionUpdateRequestModel { Name = pn, IsGranted = true });

            await _permissionService.UpdateAsync(RolePermissionValueProvider.ProviderName, "admin", permissionModels);
            await _permissionService.UpdateAsync(RolePermissionValueProvider.ProviderName, "role1", permissionModels);
            await _permissionService.UpdateAsync(UserPermissionValueProvider.ProviderName, "user3", permissionModels);
        }
    }
}
