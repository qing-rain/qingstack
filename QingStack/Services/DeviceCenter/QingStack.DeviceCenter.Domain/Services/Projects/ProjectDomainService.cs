/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectDomainService.cs
    文件功能描述：领域服务


    创建标识：QingRain - 20211108

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using QingStack.DeviceCenter.Domain.Constants;
using QingStack.DeviceCenter.Domain.Repositories;
using QingStack.DeviceCenter.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Services.Projects
{
    public class ProjectDomainService : IProjectDomainService
    {
        private readonly IRepository<Project, int> _projectRepository;

        public ProjectDomainService(IRepository<Project, int> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.InsertAsync(entity, true, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.CountAsync(where, cancellationToken);
        }

        public async Task DeleteAsync(Project entity, CancellationToken cancellationToken = default)
        {

            await _projectRepository.DeleteAsync(entity, true, cancellationToken);
        }

        public async Task<Project> FirstAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.FirstAsync(where, cancellationToken);
        }

        public async Task<Project> FirstOrDefaultAsync(Expression<Func<Project, bool>> where, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.FirstOrDefaultAsync(where, cancellationToken);
        }

        public async Task<Project> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _projectRepository.GetAsync(id, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<Project>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _projectRepository.GetListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyList<Project>> ListAsync(Expression<Func<Project, bool>> where, int pageNumber, CancellationToken cancellationToken = default)
        {
            var specification = new ProjectPagingSpecification(pageNumber, PagingConstants.DefaultPageSize);

            var specificationBuilder = new SpecificationBuilder<Project>(specification);

            specificationBuilder.Where(where).OrderByDescending(e => e.CreationTime);

            return await _projectRepository.GetListAsync(specification, cancellationToken);
        }

        public async Task UpdateAsync(Project entity, CancellationToken cancellationToken = default)
        {
            await _projectRepository.UpdateAsync(entity, true, cancellationToken);
        }
    }
}
