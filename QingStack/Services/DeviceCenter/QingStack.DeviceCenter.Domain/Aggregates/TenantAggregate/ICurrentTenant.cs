/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ICurrentTenant.cs
    文件功能描述：当前租户对外访问接口


    创建标识：QingRain - 20211110


 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public interface ICurrentTenant
    {

        Guid? Id { get; }

        /// <summary>
        /// 切换租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IDisposable Change(Guid? id);
    }
}
