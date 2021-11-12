/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGroupModel.cs
    文件功能描述：权限分组DTO

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionGroupModel
    {
        /// <summary>
        /// 权限组名
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 权限组显示名
        /// </summary>
        public string DisplayName { get; set; } = null!;
        /// <summary>
        /// 子权限组
        /// </summary>

        public List<PermissionGrantModel> Permissions { get; set; } = null!;
    }
}
