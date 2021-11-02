/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IIntegrationEventHandler.cs
    文件功能描述：集成事件处理器


    创建标识：QingRain - 2021102
 ----------------------------------------------------------------*/
using QingStack.EventBus.Event;
using System.Threading.Tasks;

namespace QingStack.EventBus.Abstractions
{
    /// <summary>
    /// 空接口标志处理器
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<in TintegrationEvent> : IIntegrationEventHandler where TintegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TintegrationEvent @event);
    }
}
