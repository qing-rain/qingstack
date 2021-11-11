/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectDeletedSendEmailDomainEventHandler.cs
    文件功能描述：项目删除触发发送Email事件


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using MediatR;
using QingStack.DeviceCenter.Domain.Events.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.DomainEventHandlers.Projects.ProjectDeleted
{
    public class ProjectDeletedSendEmailDomainEventHandler : INotificationHandler<ProjectDeletedDomainEvent>
    {
        public Task Handle(ProjectDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
