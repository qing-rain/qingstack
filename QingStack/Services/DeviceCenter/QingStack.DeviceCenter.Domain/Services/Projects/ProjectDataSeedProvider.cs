/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectDataSeedProvider.cs
    文件功能描述：演示数据


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Services.Projects
{
    public class ProjectDataSeedProvider : IDataSeedProvider
    {
        private readonly IRepository<Project, int> _projectRepository;

        public ProjectDataSeedProvider(IRepository<Project, int> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            if (await _projectRepository.GetCountAsync() <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    var project = new Project { Name = $"Project{i}" };
                    await _projectRepository.InsertAsync(project);
                }

                await _projectRepository.UnitOfWork.SaveChangesAsync();
            }
        }
    }
}
