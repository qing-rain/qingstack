/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionsController.cs
    文件功能描述：权限控制器

    创建标识：QingRain - 20211112
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QingStack.DeviceCenter.Application.Models.Permissions;
using QingStack.DeviceCenter.Application.PermissionProviders;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionApplicationService _permissionApplicationService;

        public PermissionsController(IPermissionApplicationService permissionApplicationService)
        {
            _permissionApplicationService = permissionApplicationService;
        }

        [HttpGet]
        [Authorize(PermissionPermissions.Permissions.Get)]
        public virtual Task<PermissionListResponseModel> GetAsync(string providerName, string providerKey)
        {
            return _permissionApplicationService.GetAsync(providerName, providerKey);
        }

        [HttpPut]
        [Authorize(PermissionPermissions.Permissions.Edit)]
        public virtual Task UpdateAsync(string providerName, string providerKey, IEnumerable<PermissionUpdateRequestModel> model)
        {
            return _permissionApplicationService.UpdateAsync(providerName, providerKey, model);
        }
    }
}
