/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductsController.cs
    文件功能描述：项目控制器


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "role2")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplicationService _productService;

        public ProductsController(IProductApplicationService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<PagedResponseModel<ProductGetResponseModel>> Get([FromQuery] PagedRequestModel model)
        {
            return await _productService.GetListAsync(model);
        }

        [HttpGet("{id}")]
        public async Task<ProductGetResponseModel> Get(Guid id)
        {
            return await _productService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ProductGetResponseModel> Post([FromBody] ProductCreateOrUpdateRequestModel value)
        {
            return await _productService.CreateAsync(value);
        }
        [HttpPut("{id}")]
        public async Task<ProductGetResponseModel> Put(Guid id, [FromBody] ProductCreateOrUpdateRequestModel value)
        {
            value.Id = id;
            return await _productService.UpdateAsync(value);
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}
