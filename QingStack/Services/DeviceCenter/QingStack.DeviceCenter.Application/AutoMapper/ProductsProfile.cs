/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductsProfile.cs
    文件功能描述：产品映射配置


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using AutoMapper;
using QingStack.DeviceCenter.Application.Models.Products;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace QingStack.DeviceCenter.Application.AutoMapper
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductGetResponseModel>();
            CreateMap<ProductCreateOrUpdateRequestModel, Product>();
        }
    }
}
