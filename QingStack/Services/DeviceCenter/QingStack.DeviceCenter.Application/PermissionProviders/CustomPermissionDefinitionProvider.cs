/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomPermissionDefinitionProvider.cs
    文件功能描述：自定义权限定义提供者


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using Microsoft.Extensions.Localization;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System.Reflection;

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public class CustomPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        private readonly IStringLocalizer _localizer;

        public CustomPermissionDefinitionProvider(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Permissions.CustomPermission", Assembly.GetExecutingAssembly().FullName!);
        }

        public void Define(PermissionDefinitionContext context)
        {
            var productGroup = context.AddGroup(CustomPermissions.GroupName, _localizer["Welcome"]);

            var productManagement = productGroup.AddPermission(CustomPermissions.Products.Default, _localizer["Permission:ProductStore.Products"]);

            productManagement.AddChild(CustomPermissions.Products.Create, _localizer["Permission:ProductStore.Products.Creeate"]);
            productManagement.AddChild(CustomPermissions.Products.Edit, _localizer["Permission:ProductStore.Products.Edit"]);
            productManagement.AddChild(CustomPermissions.Products.Delete, _localizer["Permission:ProductStore.Products.Delete"]);
        }
    }
}
