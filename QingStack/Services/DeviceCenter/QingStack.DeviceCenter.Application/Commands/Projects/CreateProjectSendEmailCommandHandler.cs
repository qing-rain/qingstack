/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CreateProjectSendEmailCommandHandler.cs
    文件功能描述：创建项目发送邮件命令处理程序


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using MediatR;
using QingStack.DeviceCenter.Application.Models.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Commands.Projects
{
    public class CreateProjectSendEmailCommandHandler : IRequestHandler<CreateProjectCommand, ProjectGetResponseModel>
    {
        public async Task<ProjectGetResponseModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new ProjectGetResponseModel();
        }
    }
}
