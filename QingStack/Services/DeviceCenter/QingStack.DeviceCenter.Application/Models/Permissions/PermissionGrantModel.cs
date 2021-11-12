/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGrantModel.cs
    文件功能描述：权限授予DTO

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionGrantModel
    {
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 权限显示名
        /// </summary>
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// 父级权限名
        /// </summary>
        public string ParentName { get; set; } = null!;

        /// <summary>
        /// 是否授予
        /// </summary>
        public bool IsGranted { get; set; }
        /// <summary>
        /// 权限授予提供者
        /// </summary>
        public List<string> AllowedProviders { get; set; } = null!;
    }
}
