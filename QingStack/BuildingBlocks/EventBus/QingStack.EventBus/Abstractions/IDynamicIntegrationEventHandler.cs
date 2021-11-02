/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IDynamicIntegrationEventHandler.cs
    文件功能描述：动态集成事件处理器


    创建标识：QingRain - 20211102
 ----------------------------------------------------------------*/
using System.Threading.Tasks;

namespace QingStack.EventBus.Abstractions
{
    /// <summary>
    /// 动态事件处理器
    /// </summary>
    public interface IDynamicIntegrationEventHandler : IIntegrationEventHandler
    {
        Task HandleAsync(decimal eventData);
    }
}
