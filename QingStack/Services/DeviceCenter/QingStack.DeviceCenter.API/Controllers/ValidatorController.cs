/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ValidatorController.cs
    文件功能描述：验证器控制器 手动调用验证器


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/

using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using QingStack.DeviceCenter.Application.Models.Projects;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        private readonly IValidator<ProjectCreateOrUpdateRequestModel> _validator;

        public ValidatorController(IValidator<ProjectCreateOrUpdateRequestModel> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectGetResponseModel>> Post([FromBody] ProjectCreateOrUpdateRequestModel value)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(value);

            if (validationResult.IsValid)
            {
                return await Task.FromResult(new ProjectGetResponseModel());
            }

            return BadRequest(validationResult);
        }
    }
}
