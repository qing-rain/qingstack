/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CurrentTenant.cs
    文件功能描述：当前租户对外访问器


    创建标识：QingRain - 20211110


 ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate
{
    public class CurrentTenant : ICurrentTenant
    {
        private readonly ICurrentTenantAccessor _currentTenantAccessor;

        public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor) => _currentTenantAccessor = currentTenantAccessor;

        public virtual Guid? Id => _currentTenantAccessor.TenantId;

        public IDisposable Change(Guid? id)
        {
            var parentScope = _currentTenantAccessor.TenantId;
            _currentTenantAccessor.TenantId = id;

            return new DisposeAction(() =>
            {
                _currentTenantAccessor.TenantId = parentScope;
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
