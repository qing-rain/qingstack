﻿/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomAuthorizationPolicyProvider.cs
    文件功能描述：自定义授权策略提供者


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using QingStack.DeviceCenter.Application.Services.Permissions;
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Extensions.Authorization
{
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IPermissionDefinitionManager permissionDefinitionManager) : base(options)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
        }

        public async override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            //原生授权策略提供者
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            var permission = _permissionDefinitionManager.GetOrNull(policyName);

            if (permission is not null)
            {
                //生成策略
                var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
                policyBuilder.Requirements.Add(new OperationAuthorizationRequirement { Name = policyName });

                return policyBuilder.Build();
            }

            return null;
        }
    }
}
