/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectPagedRequestModel.cs
    文件功能描述：项目查询模型


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;

namespace QingStack.DeviceCenter.Application.Queries.Projects
{
    public class ProjectPagedRequestModel : PagedRequestModel
    {
        public string? Keyword { get; set; }
    }
}
