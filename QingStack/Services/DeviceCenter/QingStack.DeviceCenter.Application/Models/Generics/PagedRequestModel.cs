/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PagedRequestModel.cs
    文件功能描述：分页请求模型


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211115
    修改描述：增加排序解释器
 ----------------------------------------------------------------*/

using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Domain.Constants;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Application.Models.Generics
{
    public class PagedRequestModel
    {
        public virtual IEnumerable<SortingDescriptor>? Sorter { get; set; }

        public virtual int PageNumber { get; set; } = 1;

        public virtual int PageSize { get; set; } = PagingConstants.DefaultPageSize;
    }
}
