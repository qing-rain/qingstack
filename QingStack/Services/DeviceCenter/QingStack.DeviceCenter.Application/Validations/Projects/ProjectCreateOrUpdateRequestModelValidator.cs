/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectCreateOrUpdateRequestModelValidator.cs
    文件功能描述：项目创建/更新模型验证器


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using FluentValidation;
using QingStack.DeviceCenter.Application.Models.Projects;

namespace QingStack.DeviceCenter.Application.Validations.Projects
{
    public class ProjectCreateOrUpdateRequestModelValidator : AbstractValidator<ProjectCreateOrUpdateRequestModel>
    {
        public ProjectCreateOrUpdateRequestModelValidator()
        {
            RuleFor(m => m.Name).NotNull().NotEmpty().Length(3, 8).WithName("ProjectName");
        }
    }
}
