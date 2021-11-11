/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductCreateOrUpdateRequestModel.cs
    文件功能描述：项目创建/更新模型


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Application.Models.Products
{
    public class ProductCreateOrUpdateRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
