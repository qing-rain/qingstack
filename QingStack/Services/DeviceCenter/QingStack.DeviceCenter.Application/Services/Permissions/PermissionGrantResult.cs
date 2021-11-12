/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGrantResult.cs
    文件功能描述：权限授权结果

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    public enum PermissionGrantResult
    {
        /// <summary>
        /// 未知
        /// </summary>
        Undefined,
        /// <summary>
        /// 授予
        /// </summary>
        Granted,
        /// <summary>
        /// 拒绝
        /// </summary>
        Prohibited
    }
}
