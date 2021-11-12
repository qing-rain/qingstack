/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionUpdateRequestModel.cs
    文件功能描述：权限更新DTO

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.Models.Permissions
{
    public class PermissionUpdateRequestModel
    {
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 是否授予
        /// </summary>
        public bool IsGranted { get; set; }
    }
}
