/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ICurrentTenantAccessor.cs
    文件功能描述：当前租户访问器


    创建标识：QingRain - 20211110

    修改标识：QingRain - 20211114
    修改描述：重构接口 返回租户信息
 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public interface ICurrentTenantAccessor
    {
        TenantInfo? Current { get; set; }
    }
}
