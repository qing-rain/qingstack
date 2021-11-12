/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionListResponseModel.cs
    文件功能描述：权限列表DTO

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionListResponseModel
    {
        /// <summary>
        /// 权限显示名
        /// </summary>
        public string EntityDisplayName { get; set; } = null!;

        public List<PermissionGroupModel> Groups { get; set; } = null!;
    }
}
