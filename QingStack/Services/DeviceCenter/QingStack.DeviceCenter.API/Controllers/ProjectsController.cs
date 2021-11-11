/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectsController.cs
    文件功能描述：项目控制器


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Application.Services.Generics;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel> _crudService;

        public ProjectsController(ICrudApplicationService<int, ProjectGetResponseModel, PagedRequestModel, ProjectGetResponseModel, ProjectCreateOrUpdateRequestModel, ProjectCreateOrUpdateRequestModel> crudService)
        {
            _crudService = crudService;
        }

        // GET: api/<ProjectsController>
        [HttpGet]
        public async Task<PagedResponseModel<ProjectGetResponseModel>> Get([FromQuery] PagedRequestModel model)
        {
            return await _crudService.GetListAsync(model);
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ProjectGetResponseModel> Get(int id)
        {
            return await _crudService.GetAsync(id);
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task<ProjectGetResponseModel> Post([FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            return await _crudService.CreateAsync(value);
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public async Task<ProjectGetResponseModel> Put(int id, [FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            value.Id = id;
            return await _crudService.UpdateAsync(value);
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _crudService.DeleteAsync(id);
        }
    }
}
