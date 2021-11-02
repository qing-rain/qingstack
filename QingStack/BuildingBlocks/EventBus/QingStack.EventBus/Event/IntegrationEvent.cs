/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IntegrationEvent.cs
    文件功能描述：集成事件抽象基类


    创建标识：QingRain - 20211102
 ----------------------------------------------------------------*/
using System;

namespace QingStack.EventBus.Event
{
    /// <summary>
    /// 集成事件
    /// </summary>
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTimeOffset.Now;
        }
        public IntegrationEvent(Guid id, DateTimeOffset createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
