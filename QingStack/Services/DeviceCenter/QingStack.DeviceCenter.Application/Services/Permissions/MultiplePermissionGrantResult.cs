/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：MultiplePermissionGrantResult.cs
    文件功能描述：多权限授予结果

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace QingStack.DeviceCenter.Application.Services.Permissions
{
    /// <summary>
    /// 多权限授予结果
    /// </summary>
    public class MultiplePermissionGrantResult
    {
        /// <summary>
        /// 所有都通过则通过
        /// </summary>
        public bool AllGranted => Result.Values.All(x => x == PermissionGrantResult.Granted);

        /// <summary>
        /// 所有都拒绝则拒绝
        /// </summary>
        public bool AllProhibited => Result.Values.All(x => x == PermissionGrantResult.Prohibited);
        /// <summary>
        /// 权限授予结果列表
        /// </summary>
        public Dictionary<string, PermissionGrantResult> Result { get; }

        public MultiplePermissionGrantResult() => Result = new();

        public MultiplePermissionGrantResult(string[] names, PermissionGrantResult grantResult = PermissionGrantResult.Undefined) : this()
        {
            if (names is null)
            {
                throw new ArgumentNullException(nameof(names));
            }

            Array.ForEach(names, name => Result.Add(name, grantResult));
        }
    }
}
