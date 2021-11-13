/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ICurrentTenant.cs
    文件功能描述：当前租户对外访问接口


    创建标识：QingRain - 20211110

    修改标识：QingRain - 20211114
    修改描述：重构接口 返回当前租户信息
 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public interface ICurrentTenant
    {

        /// <summary>
        /// 当前租户是否可用
        /// </summary>
        bool IsAvailable { get; }

        Guid? Id { get; }

        string? Name { get; }

        /// <summary>
        /// 切换租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IDisposable Change(Guid? id, string? name = null);
    }
}
