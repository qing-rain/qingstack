/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductCreatedDomainEvent.cs
    文件功能描述：产品创建领域事件


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using MediatR;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace QingStack.DeviceCenter.Domain.Events.Products
{
    public class ProductCreatedDomainEvent : INotification
    {
        public Product Product { get; set; } = null!;

        public ProductCreatedDomainEvent(Product product) => Product = product;
    }
}
