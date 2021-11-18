/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductsController.cs
    文件功能描述：项目控制器


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Mvc;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Products;
using QingStack.DeviceCenter.Application.Services.Products;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "role2")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplicationService _productService;

        public ProductsController(IProductApplicationService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        //[Authorize(ProductPermissions.Products.Default)]
        public async Task<PagedResponseModel<ProductGetResponseModel>> GetProducts([FromQuery] ProductPagedRequestModel model)
        {
            return await _productService.GetListAsync(model);
        }

        [HttpGet("{id}")]
        //[Authorize(ProductPermissions.Products.Default)]
        public async Task<ProductGetResponseModel> GetProduct(Guid id)
        {
            return await _productService.GetAsync(id);
        }

        [HttpPost]
        //[Authorize(ProductPermissions.Products.Create)]
        public async Task<ProductGetResponseModel> PostProduct([FromBody] ProductCreateOrUpdateRequestModel value)
        {
            return await _productService.CreateAsync(value);
        }
        [HttpPut("{id}")]
        //[Authorize(ProductPermissions.Products.Edit)]
        public async Task<ProductGetResponseModel> PutProduct(Guid id, [FromBody] ProductCreateOrUpdateRequestModel value)
        {
            value.Id = id;
            return await _productService.UpdateAsync(id, value);
        }
        [HttpDelete("{id}")]
        //[Authorize(ProductPermissions.Products.Delete)]
        public async Task DeleteProduct(Guid id)
        {
            //await Task.Delay(TimeSpan.FromSeconds(5));
            await _productService.DeleteAsync(id);
        }
    }
}
