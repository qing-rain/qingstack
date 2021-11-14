/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CreateProjectCommandHandler.cs
    文件功能描述：创建项目命令处理程序


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using AutoMapper;
using MediatR;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using QingStack.DeviceCenter.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Commands.Projects
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectGetResponseModel>
    {
        private readonly IRepository<Project> _projectRepository;

        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectGetResponseModel> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            Project project = _mapper.Map<Project>(request);
            project = await _projectRepository.InsertAsync(project, true, cancellationToken);
            return _mapper.Map<ProjectGetResponseModel>(project);
        }
    }
}
