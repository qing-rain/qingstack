/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CurrentTenantAccessor.cs
    文件功能描述：当前租户访问器


    创建标识：QingRain - 20211110


 ----------------------------------------------------------------*/
using System;
using System.Threading;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class CurrentTenantAccessor : ICurrentTenantAccessor
    {
        //线程异步本地存储
        private readonly AsyncLocal<Guid?> _currentScope = new();

        public Guid? TenantId { get => _currentScope.Value; set => _currentScope.Value = value; }
    }
}
