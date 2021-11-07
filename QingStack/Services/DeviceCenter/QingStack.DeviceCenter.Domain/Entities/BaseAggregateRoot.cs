/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：BaseAggregateRoot.cs
    文件功能描述：基本聚合根


    创建标识：QingRain - 20211107
 ----------------------------------------------------------------*/
using MediatR;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Entities
{
    public abstract class BaseAggregateRoot : BaseEntity, IAggregateRoot, IDomainEvents
    {
        /// <summary>
        /// 只读集合，保证事件完整性（面向对象封装完整性）
        /// </summary>
        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        public void ClearDomainEvents() => _domainEvents?.Clear();
    }

    public abstract class BaseAggregateRoot<Tkey> : BaseEntity<Tkey>, IAggregateRoot, IDomainEvents
    {
        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        public void ClearDomainEvents() => _domainEvents?.Clear();
    }
}
