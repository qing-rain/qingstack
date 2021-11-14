/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CreateProjectIdentifiedCommandHandler.cs
    文件功能描述：创建项目去重命令处理程序


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using MediatR;
using QingStack.DeviceCenter.Application.Infrastructure.Command;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Infrastructure.Idempotency;

namespace QingStack.DeviceCenter.Application.Commands.Projects
{
    public class CreateProjectIdentifiedCommandHandler : IdentifiedCommandHandler<CreateProjectCommand, ProjectGetResponseModel>
    {
        public CreateProjectIdentifiedCommandHandler(IMediator mediator, IRequestManager requestManager) : base(mediator, requestManager)
        {
        }

        /// <summary>
        /// // Ignore duplicate requests for processing.
        /// </summary>
        protected override ProjectGetResponseModel? CreateResultForDuplicateRequest() => new();
    }
}
