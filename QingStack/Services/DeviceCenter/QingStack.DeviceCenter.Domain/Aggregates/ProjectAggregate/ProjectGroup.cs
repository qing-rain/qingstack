/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectGroup.cs
    文件功能描述：项目组实体


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate
{
    public class ProjectGroup : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public ProjectGroup Parent { get; set; } = null!;

        public List<ProjectGroup> Children { get; set; } = null!;

        public int? ParentId { get; set; }

        public string? Remark { get; set; }

        public int? Sorting { get; set; }

        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;
    }
}
