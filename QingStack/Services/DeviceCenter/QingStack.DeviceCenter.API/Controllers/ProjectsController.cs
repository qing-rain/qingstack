/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectsController.cs
    文件功能描述：项目控制器


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211114
    创建描述：注入项目查询接口,调整Get查询调用方式

    修改标识：QingRain - 20211114
    创建描述：使用 MediatR 实现 Command 命令模式
 ----------------------------------------------------------------*/
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingStack.DeviceCenter.Application.Commands.Projects;
using QingStack.DeviceCenter.Application.Infrastructure.Command;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Application.PermissionProviders;
using QingStack.DeviceCenter.Application.Queries.Projects;
using QingStack.DeviceCenter.Application.Services.Generics;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "role1")]
    public class ProjectsController : ControllerBase
    {
        private readonly ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel> _crudService;
        private readonly IProjectQueries _projectQueries;
        private readonly IMediator _mediator;
        public ProjectsController(ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel> crudService, IProjectQueries projectQueries, IMediator mediator)
        {
            _crudService = crudService;
            _projectQueries = projectQueries;
            _mediator = mediator;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        [Authorize(ProjectPermissions.Projects.Default)]
        public async Task<PagedResponseModel<ProjectGetResponseModel>> Get([FromQuery] ProjectPagedRequestModel model)
        {
            return await _projectQueries.GetProjectsAsync(model);
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        [Authorize(ProjectPermissions.Projects.Default)]
        public async Task<ProjectGetResponseModel> Get(int id)
        {
            return await _projectQueries.GetProjectAsync(id);
        }

        // POST api/<ProjectsController>
        [HttpPost]
        [Authorize(ProjectPermissions.Projects.Create)]
        public async Task<ProjectGetResponseModel> Post([FromBody] CreateProjectCommand command, [FromHeader(Name = "X-Request-Id")] string? requestId)
        {
            //使用去重命令处理器分发子命令
            requestId ??= Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var identifiedCommand = new IdentifiedCommand<CreateProjectCommand, ProjectGetResponseModel>(command, requestId);

            ProjectGetResponseModel result = await _mediator.Send(identifiedCommand);

            return result;
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        [Authorize(ProjectPermissions.Projects.Edit)]
        public async Task<ProjectGetResponseModel> Put(int id, [FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            value.Id = id;
            return await _crudService.UpdateAsync(id, value);
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        [Authorize(ProjectPermissions.Projects.Delete)]
        public async Task Delete(int id)
        {
            await _crudService.DeleteAsync(id);
        }
    }
}
