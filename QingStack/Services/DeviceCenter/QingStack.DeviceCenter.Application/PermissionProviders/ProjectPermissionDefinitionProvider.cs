/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectPermissionDefinitionProvider.cs
    文件功能描述：项目权限定义提供者


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using Microsoft.Extensions.Localization;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System.Reflection;

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public class ProjectPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        private readonly IStringLocalizer _localizer;

        public ProjectPermissionDefinitionProvider(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Welcome", Assembly.GetExecutingAssembly().FullName ?? string.Empty);
        }

        public void Define(PermissionDefinitionContext context)
        {
            var productGroup = context.AddGroup(ProjectPermissions.GroupName, _localizer["Permission:ProjectManager"]);

            var productManagement = productGroup.AddPermission(ProjectPermissions.Projects.Default, _localizer["Permission:ProjectStore.Projects"]);

            productManagement.AddChild(ProjectPermissions.Projects.Create, _localizer["Permission:ProjectManager.Projects.Creeate"]);
            productManagement.AddChild(ProjectPermissions.Projects.Edit, _localizer["Permission:ProjectManager.Projects.Edit"]);
            productManagement.AddChild(ProjectPermissions.Projects.Delete, _localizer["Permission:ProjectManager.Projects.Delete"]);
        }
    }
}
