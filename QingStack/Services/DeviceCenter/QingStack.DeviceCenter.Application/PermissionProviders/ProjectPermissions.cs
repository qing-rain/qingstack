/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectPermissions.cs
    文件功能描述：项目常量


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public static class ProjectPermissions
    {
        public const string GroupName = "ProjectManager";

        public static class Projects
        {
            public const string Default = GroupName + ".Projects";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
