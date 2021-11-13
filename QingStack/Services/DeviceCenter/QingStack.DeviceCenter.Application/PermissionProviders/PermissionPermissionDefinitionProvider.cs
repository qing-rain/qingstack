/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionPermissionDefinitionProvider.cs
    文件功能描述：权限权限定义提供者


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using Microsoft.Extensions.Localization;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System.Reflection;

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public class PermissionPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        private readonly IStringLocalizer _localizer;

        public PermissionPermissionDefinitionProvider(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Welcome", Assembly.GetExecutingAssembly().FullName ?? string.Empty);
        }

        public void Define(PermissionDefinitionContext context)
        {
            var productGroup = context.AddGroup(PermissionPermissions.GroupName, _localizer["Permission:PermissionManager"]);

            var productManagement = productGroup.AddPermission(PermissionPermissions.Permissions.Default, _localizer["Permission:PermissionStore.Permissions"]);

            productManagement.AddChild(PermissionPermissions.Permissions.Get, _localizer["Permission:PermissionManager.Permissions.Get"]);
            productManagement.AddChild(PermissionPermissions.Permissions.Edit, _localizer["Permission:PermissionManager.Permissions.Edit"]);
        }
    }
}
