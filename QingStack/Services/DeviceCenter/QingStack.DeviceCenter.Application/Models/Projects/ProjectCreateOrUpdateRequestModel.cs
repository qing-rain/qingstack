/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectCreateOrUpdateRequestModel.cs
    文件功能描述：项目创建\更新模型


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using System;
using System.ComponentModel.DataAnnotations;

namespace QingStack.DeviceCenter.Application.Models.Projects
{
    public class ProjectCreateOrUpdateRequestModel
    {
        public int Id { get; set; }

        [StringLength(8, ErrorMessage = "The {0} must be at least {2} characte long.", MinimumLength = 6)]
        [Display(Name = "ProjectName")]
        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
