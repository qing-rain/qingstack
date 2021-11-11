/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PagedRequestModel.cs
    文件功能描述：分页请求模型


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/

namespace QingStack.DeviceCenter.Application.Models.Generics
{
    public class PagedRequestModel
    {
        public virtual string? Sorting { get; set; }

        public virtual int PageNumber { get; set; }

        public virtual int PageSize { get; set; }
    }
}
