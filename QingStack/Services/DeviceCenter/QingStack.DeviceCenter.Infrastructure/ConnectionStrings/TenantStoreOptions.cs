/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantStoreOptions.cs
    文件功能描述：租户仓储选项


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Infrastructure.ConnectionStrings
{
    public class TenantStoreOptions
    {
        public TenantConfiguration[]? Tenants { get; set; }
    }
}
