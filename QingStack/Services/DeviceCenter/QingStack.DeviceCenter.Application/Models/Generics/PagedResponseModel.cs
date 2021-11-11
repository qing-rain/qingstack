/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PagedResponseModel.cs
    文件功能描述：通用返回结果模型


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Application.Models.Generics
{
    public class PagedResponseModel<T>
    {
        public PagedResponseModel(IReadOnlyList<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public IReadOnlyList<T> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
