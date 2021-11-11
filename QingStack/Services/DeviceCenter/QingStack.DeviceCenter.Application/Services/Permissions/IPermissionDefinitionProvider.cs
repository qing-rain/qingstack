/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionDefinitionProvider.cs
    文件功能描述：权限定义提供者


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public interface IPermissionDefinitionProvider
    {
        void Define(PermissionDefinitionContext context);
    }
}
