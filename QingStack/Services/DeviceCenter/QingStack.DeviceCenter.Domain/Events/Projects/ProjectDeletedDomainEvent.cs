/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectDeletedDomainEvent.cs
    文件功能描述：项目删除领域事件


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using MediatR;

namespace QingStack.DeviceCenter.Domain.Events.Projects
{
    public class ProjectDeletedDomainEvent : INotification
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; } = null!;
    }
}
