/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ClientRequest.cs
    文件功能描述：客户端请求模型


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using System;
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Infrastructure.Idempotency
{
    public class ClientRequest
    {
        [AllowNull]
        public string Id { get; set; }

        [AllowNull]
        public string Name { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
