/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：LoggingBehavior.cs
    文件功能描述：日志记录行为拦截器


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.Extensions.Logging;
using QingStack.EventBus.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            var response = await next();

            _logger.LogInformation("Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }
}
