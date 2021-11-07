/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：Project.cs
    文件功能描述：项目聚合根


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate
{
    public class Project : BaseAggregateRoot<int>
    {
        public string Name { get; set; } = null!;

        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// 项目组
        /// </summary>

        public List<ProjectGroup> Groups { get; set; } = null!;
    }
}
