/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionDefinitionManager.cs
    文件功能描述：权限定义管理器


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public interface IPermissionDefinitionManager
    {
        PermissionDefinition Get([NotNull] string name);

        PermissionDefinition? GetOrNull([NotNull] string name);

        IReadOnlyList<PermissionDefinition> GetPermissions();

        IReadOnlyList<PermissionGroupDefinition> GetGroups();
    }
}
