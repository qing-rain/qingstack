/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ValidatorBehavior.cs
    文件功能描述：命令验证行为拦截器


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using QingStack.EventBus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeName = request.GetGenericTypeName();

            _logger.LogInformation("Validating command {CommandType}", typeName);

            var failures = _validators.Select(v => v.Validate(request)).SelectMany(result => result.Errors).Where(error => error != null).ToList();

            if (failures.Any())
            {
                _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);
                throw new ApplicationException($"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException("Validation exception", failures));
            }

            return await next();
        }
    }
}
