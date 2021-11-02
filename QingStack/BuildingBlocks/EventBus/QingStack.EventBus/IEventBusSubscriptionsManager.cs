/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IEventBusSubscriptionsManager.cs
    文件功能描述：事件总线订阅管理


    创建标识：QingRain - 2021103
 ----------------------------------------------------------------*/
using QingStack.EventBus.Abstractions;
using QingStack.EventBus.Event;
using System;
using System.Collections.Generic;

namespace QingStack.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;
        /// <summary>
        /// 添加动态订阅者
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void AddDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 移除动态订阅者
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 添加订阅者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        /// <summary>
        /// 移除订阅者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
        /// <summary>
        /// 是否有订阅者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HasSubscriptionsForEvent<T>()
            where T : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        /// <summary>
        /// 清空订阅者
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取订阅者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>()
            where T : IntegrationEvent;

        /// <summary>
        /// 获取动态类型订阅者
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();
    }
}
