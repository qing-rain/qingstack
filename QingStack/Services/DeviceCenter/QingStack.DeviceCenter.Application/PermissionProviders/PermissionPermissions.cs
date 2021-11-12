/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionPermissions.cs
    文件功能描述：权限常量


    创建标识：QingRain - 20211112

 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public static class PermissionPermissions
    {
        public const string GroupName = "PermissionManager";

        public static class Permissions
        {
            public const string Default = GroupName + ".Permissions";
            public const string Get = Default + ".Get";
            public const string Edit = Default + ".Edit";
        }
    }
}
