/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SubscriptionInfo.cs
    文件功能描述：订阅者


    创建标识：QingRain - 20211103
 ----------------------------------------------------------------*/
using System;

namespace QingStack.EventBus
{
    public class SubscriptionInfo
    {
        public bool IsDynamic { get; }

        public Type HandlerType { get; }

        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Dynamic(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
    }
}
