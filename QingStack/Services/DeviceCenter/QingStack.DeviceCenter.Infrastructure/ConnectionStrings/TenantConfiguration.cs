/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantConfiguration.cs
    文件功能描述：租户配置


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Infrastructure.ConnectionStrings
{
    [Serializable]
    public class TenantConfiguration
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid TenantId { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; } = string.Empty;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public Dictionary<string, string>? ConnectionStrings { get; set; }
    }
}
