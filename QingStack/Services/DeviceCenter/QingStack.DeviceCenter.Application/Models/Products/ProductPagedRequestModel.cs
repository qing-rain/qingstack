/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductPagedRequestModel.cs
    文件功能描述：产品关键字搜索分页请求模型


    创建标识：QingRain - 20211115

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;

namespace QingStack.DeviceCenter.Application.Models.Products
{
    public class ProductPagedRequestModel : PagedRequestModel
    {
        public string? Keyword { get; set; }
    }
}
