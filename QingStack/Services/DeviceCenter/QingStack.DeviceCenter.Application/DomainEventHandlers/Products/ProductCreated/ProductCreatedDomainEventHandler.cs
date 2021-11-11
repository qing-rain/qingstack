/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductCreatedDomainEventHandler.cs
    文件功能描述：产品创建触发事件处理器


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.Extensions.Logging;
using QingStack.DeviceCenter.Domain.Events.Products;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.DomainEventHandlers.Products.ProductCreated
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly ILogger<ProductCreatedDomainEventHandler> _logger;

        public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger) => _logger = logger;

        public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"ProductId={notification.Product.Id},ProductName={notification.Product.Name}");

            await Task.CompletedTask;
        }
    }
}
