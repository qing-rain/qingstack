using MediatR;
using Microsoft.Extensions.Logging;
using QingStack.EventBus.Extensions;
using System;
/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TransactionBehavior.cs
    文件功能描述：事务拦截器


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace QingStack.DeviceCenter.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger) => _logger = logger ?? throw new ArgumentException(nameof(ILogger));

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string? typeName = request?.GetGenericTypeName();

            TResponse? response = default;

            using (TransactionScope? scope = new(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    response = await next();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                    throw;
                }
            }

            return response;
        }
    }
}
