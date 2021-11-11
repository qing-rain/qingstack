/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SecurityRequirementsOperationFilter.cs
    文件功能描述：安全需要操作过滤器


    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace QingStack.DeviceCenter.API.Infrastructure.Swagger
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Add Security Definitions and Requirements
        /// https://github.com/domaindrivendev/Swashbuckle.AspNetCore#add-security-definitions-and-requirements
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasAuthorize = context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() == true || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
            bool hasAllowAnonymous = context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() == true || context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

            if (hasAuthorize && !hasAllowAnonymous)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                OpenApiSecurityScheme oAuthScheme = new()
                {
                    Reference = new() { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [oAuthScheme] =new []{ "devicecenterapi" }
                    }
                };
            }
        }
    }
}
