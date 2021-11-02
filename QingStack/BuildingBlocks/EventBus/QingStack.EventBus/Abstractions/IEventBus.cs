/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IEventBus.cs
    文件功能描述：事件总线


    创建标识：QingRain - 2021103
 ----------------------------------------------------------------*/
using QingStack.EventBus.Event;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.EventBus.Abstractions
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken = default);
        /// <summary>
        /// 强类型订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        /// <summary>
        /// 强类型取消订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        /// <summary>
        /// 动态类型订阅事件
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 动态类型取消订阅事件
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
    }
}
