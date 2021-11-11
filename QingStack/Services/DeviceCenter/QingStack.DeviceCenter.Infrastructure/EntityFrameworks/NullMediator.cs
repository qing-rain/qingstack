/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：NullMediator.cs
    文件功能描述：空中介者接口处理


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class NullMediator : IMediator
    {
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<TResponse>(default!);
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object?>(default);
        }
    }
}
