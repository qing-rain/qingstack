/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IProjectQueries.cs
    文件功能描述：项目查询接口


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Queries.Projects
{
    public interface IProjectQueries
    {
        Task<ProjectGetResponseModel> GetProjectAsync(int id);

        Task<PagedResponseModel<ProjectGetResponseModel>> GetProjectsAsync(ProjectPagedRequestModel model);
    }
}
