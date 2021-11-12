/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionRequirementHandler.cs
    文件功能描述：权限需求处理器，驱动系统进行权限授予检查

    创建标识：QingRain - 20210311
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Extensions.Authorization
{
    public class PermissionRequirementHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        private readonly IPermissionChecker _permissionChecker;

        public PermissionRequirementHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, requirement.Name))
            {
                context.Succeed(requirement);
            }
        }
    }
    public class PermissionRequirementHandler<TResource> : AuthorizationHandler<OperationAuthorizationRequirement, TResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, TResource resource)
        {
            throw new System.NotImplementedException();
        }
    }
}
