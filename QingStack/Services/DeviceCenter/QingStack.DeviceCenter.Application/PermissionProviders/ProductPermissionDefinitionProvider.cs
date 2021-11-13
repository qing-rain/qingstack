/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductPermissionDefinitionProvider.cs
    文件功能描述：产品权限定义提供者


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using Microsoft.Extensions.Localization;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System.Reflection;

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public class ProductPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        private readonly IStringLocalizer _localizer;

        public ProductPermissionDefinitionProvider(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Welcome", Assembly.GetExecutingAssembly().FullName ?? string.Empty);
        }

        public void Define(PermissionDefinitionContext context)
        {
            var productGroup = context.AddGroup(ProductPermissions.GroupName, _localizer["Permission:ProductManager"]);

            var productManagement = productGroup.AddPermission(ProductPermissions.Products.Default, _localizer["Permission:ProductStore.Products"]);

            productManagement.AddChild(ProductPermissions.Products.Create, _localizer["Permission:ProductManager.Products.Creeate"]);
            productManagement.AddChild(ProductPermissions.Products.Edit, _localizer["Permission:ProductManager.Products.Edit"]);
            productManagement.AddChild(ProductPermissions.Products.Delete, _localizer["Permission:ProductManager.Products.Delete"]);
        }
    }
}
