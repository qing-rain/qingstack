/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IRabbitMQPersistentConnection.cs
    文件功能描述：RabbitMQ可靠连接


    创建标识：QingRain - 20211103
 ----------------------------------------------------------------*/
using RabbitMQ.Client;
using System;

namespace QingStack.EventBus.RabbitMQ
{
    /// <summary>
    /// 处理连接断开情况
    /// </summary>
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        /// <summary>
        /// 是否连接状态
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 是否重连
        /// </summary>
        /// <returns></returns>
        bool TryConnect();

        IModel CreateModel();
    }
}
