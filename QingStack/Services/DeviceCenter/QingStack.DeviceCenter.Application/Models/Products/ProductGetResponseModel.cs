/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductGetResponseModel.cs
    文件功能描述：项目输出模型


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Application.Models.Products
{
    public class ProductGetResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
