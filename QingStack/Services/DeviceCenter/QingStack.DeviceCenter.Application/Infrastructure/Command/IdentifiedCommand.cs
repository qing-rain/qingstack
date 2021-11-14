/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IdentifiedCommand.cs
    文件功能描述：去重命令模型


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using MediatR;

namespace QingStack.DeviceCenter.Application.Infrastructure.Command
{
    public class IdentifiedCommand<TRequest, TResponse> : IRequest<TResponse> where TRequest : IRequest<TResponse>
    {
        public TRequest Command { get; }

        public string Id { get; set; }

        public IdentifiedCommand(TRequest command, string id)
        {
            Command = command;
            Id = id;
        }
    }
}
