/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectApplicationService.cs
    文件功能描述：项目应用服务


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using AutoMapper;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Application.Services.Generics;
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;
using QingStack.DeviceCenter.Domain.Repositories;

namespace QingStack.DeviceCenter.Application.Services.Projects
{
    public class ProjectApplicationService : CrudApplicationService<Project, int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel>
    {
        public ProjectApplicationService(IRepository<Project, int> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
