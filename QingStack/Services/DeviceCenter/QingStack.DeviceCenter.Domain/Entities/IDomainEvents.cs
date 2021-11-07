/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IDomainEvents.cs
    文件功能描述：领域事件接口


    创建标识：QingRain - 2021108
 ----------------------------------------------------------------*/
using MediatR;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Entities
{
    public interface IDomainEvents
    {
        /// <summary>
        /// 事件通知
        /// </summary>
        IReadOnlyCollection<INotification> DomainEvents { get; }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventItem"></param>
        void AddDomainEvent(INotification eventItem);

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventItem"></param>
        void RemoveDomainEvent(INotification eventItem);

        /// <summary>
        /// 清空所有事件
        /// </summary>
        void ClearDomainEvents();
    }
}
