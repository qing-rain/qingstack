/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CurrentTenantAccessor.cs
    文件功能描述：当前租户访问器


    创建标识：QingRain - 20211110

    修改标识：QingRain - 20211114
    修改描述：重构 返回租户信息
 ----------------------------------------------------------------*/
using System.Threading;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class CurrentTenantAccessor : ICurrentTenantAccessor
    {
        //线程异步本地存储
        private readonly AsyncLocal<TenantInfo?> _currentScope = new();

        public TenantInfo? Current { get => _currentScope.Value; set => _currentScope.Value = value; }

        public CurrentTenantAccessor() => _currentScope = new AsyncLocal<TenantInfo?>();
    }
}
