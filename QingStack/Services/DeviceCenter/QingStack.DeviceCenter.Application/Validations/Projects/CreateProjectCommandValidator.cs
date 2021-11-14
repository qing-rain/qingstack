/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CreateProjectCommandValidator.cs
    文件功能描述：创建项目命令校验器


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using FluentValidation;
using QingStack.DeviceCenter.Application.Commands.Projects;

namespace QingStack.DeviceCenter.Application.Validations.Projects
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(m => m.Name).NotNull().NotEmpty().Length(5, 20);
        }
    }
}
