/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductPermissions.cs
    文件功能描述：产品常量


    创建标识：QingRain - 20211113

 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.PermissionProviders
{
    public static class ProductPermissions
    {
        public const string GroupName = "ProductManager";

        public static class Products
        {
            public const string Default = GroupName + ".Products";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}
