/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomPermissions.cs
    文件功能描述：自定义权限


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public static class CustomPermissions
    {
        public const string GroupName = "ProductStore";

        public static class Products
        {
            public const string Default = GroupName + ".Products";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
