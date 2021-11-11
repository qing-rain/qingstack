/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectCreateOrUpdateRequestModelValidator.cs
    文件功能描述：项目创建/更新模型验证器


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211111
    修改描述：链式验证模型本地化

 ----------------------------------------------------------------*/
using FluentValidation;
using Microsoft.Extensions.Localization;
using QingStack.DeviceCenter.Application.Models.Projects;
using System.Reflection;

namespace QingStack.DeviceCenter.Application.Validations.Projects
{
    public class ProjectCreateOrUpdateRequestModelValidator : AbstractValidator<ProjectCreateOrUpdateRequestModel>
    {
        public ProjectCreateOrUpdateRequestModelValidator(IStringLocalizerFactory factory)
        {
            //指定资源位置
            IStringLocalizer _localizer1 = factory.Create("Models.Projects.ProjectCreateOrUpdateRequestModel", Assembly.GetExecutingAssembly().ToString());
            //根据命名空间自动匹配资源
            IStringLocalizer _localizer2 = factory.Create(typeof(ProjectCreateOrUpdateRequestModel));

            RuleFor(m => m.Name).Length(4, 7).WithMessage((m, p) => _localizer1["LengthValidator", _localizer1["ProjectName"], 5, 7, p.Length]);
            RuleFor(m => m.Name).NotNull().NotEmpty().Length(3, 8).WithName(_localizer2["ProjectName"]);
        }
    }
}
