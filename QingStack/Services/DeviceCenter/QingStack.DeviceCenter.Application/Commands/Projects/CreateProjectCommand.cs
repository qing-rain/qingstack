/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CreateProjectCommand.cs
    文件功能描述：创建项目命令模型


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using MediatR;
using QingStack.DeviceCenter.Application.Models.Projects;
using System;

namespace QingStack.DeviceCenter.Application.Commands.Projects
{
    public class CreateProjectCommand : IRequest<ProjectGetResponseModel>
    {
        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; }
    }
}
