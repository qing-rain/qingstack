/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectPagingSpecification.cs
    文件功能描述：分页规约


    创建标识：QingRain - 2021108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using QingStack.DeviceCenter.Domain.Specifications;
using System;
using System.Linq;

namespace QingStack.DeviceCenter.Domain.Services.Projects
{
    public class ProjectPagingSpecification : Specification<Project>
    {
        public ProjectPagingSpecification(int pageNumber, int pageSize)
        {
            Query.Where(e => e.CreationTime > DateTimeOffset.Now.AddDays(-1));
            Query.Include(e => e.Groups);
            Query.Include(e => e.Groups).ThenInclude(e => e.Count);
            Query.OrderBy(e => e.Id);
            Query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
