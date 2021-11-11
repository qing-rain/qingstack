/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectGetResponseModel.cs
    文件功能描述：项目查询模型


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using System;

namespace QingStack.DeviceCenter.Application.Models.Projects
{
    public class ProjectGetResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
