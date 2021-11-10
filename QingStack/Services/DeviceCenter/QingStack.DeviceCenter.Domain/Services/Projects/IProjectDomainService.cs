/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IProjectDomainService.cs
    文件功能描述：领域服务


    创建标识：QingRain - 20211108
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Services.Projects
{
    public interface IProjectDomainService
    {
        Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Project>> ListAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Project>> ListAsync(Expression<Func<Project, bool>> where, int pageNumber, CancellationToken cancellationToken = default);

        Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(Project entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(Project entity, CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);

        Task<Project> FirstAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);

        Task<Project> FirstOrDefaultAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default);
    }
}
