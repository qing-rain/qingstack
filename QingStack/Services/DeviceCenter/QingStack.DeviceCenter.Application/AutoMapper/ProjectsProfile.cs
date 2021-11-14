/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectsProfile.cs
    文件功能描述：项目映射配置


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211114
    创建描述：增加创建项目命令模型与实体映射
 ----------------------------------------------------------------*/
using AutoMapper;
using QingStack.DeviceCenter.Application.Commands.Projects;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace QingStack.DeviceCenter.Application.AutoMapper
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<Project, ProjectGetResponseModel>();
            CreateMap<ProjectCreateOrUpdateRequestModel, Project>();
            CreateMap<CreateProjectCommand, Project>();
        }
    }
}
