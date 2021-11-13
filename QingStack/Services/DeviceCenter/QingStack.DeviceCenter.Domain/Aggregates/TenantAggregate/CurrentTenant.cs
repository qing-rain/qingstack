/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CurrentTenant.cs
    文件功能描述：当前租户对外访问器


    创建标识：QingRain - 20211110


 ----------------------------------------------------------------*/
using System;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class CurrentTenant : ICurrentTenant
    {
        private readonly ICurrentTenantAccessor _currentTenantAccessor;
        /// <summary>
        /// 租户ID是否有值
        /// </summary>
        public virtual bool IsAvailable => Id.HasValue;

        public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor) => _currentTenantAccessor = currentTenantAccessor;

        public virtual Guid? Id => _currentTenantAccessor.Current?.TenantId;

        public string? Name => _currentTenantAccessor.Current?.Name;

        public IDisposable Change(Guid? id, string? name = null)
        {
            var parentScope = _currentTenantAccessor.Current;
            _currentTenantAccessor.Current = new TenantInfo(id, name);

            return new DisposeAction(() =>
            {
                _currentTenantAccessor.Current = parentScope;
            });
        }
        //类中类
        public class DisposeAction : IDisposable
        {
            private readonly Action _action;

            public DisposeAction([NotNull] Action action) => _action = action;

            void IDisposable.Dispose()
            {
                _action();
                GC.SuppressFinalize(this);
            }
        }
    }
}
