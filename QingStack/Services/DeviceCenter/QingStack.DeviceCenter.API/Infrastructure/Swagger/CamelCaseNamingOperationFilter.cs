/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CamelCaseNamingOperationFilter.cs
    文件功能描述：参数转换过滤器


    创建标识：QingRain - 20211115
 ----------------------------------------------------------------*/
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Text.Json;

namespace QingStack.DeviceCenter.API.Infrastructure.Swagger
{
    public class CamelCaseNamingOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters?.ToList()?.ForEach(op =>
            {
                op.Name = JsonNamingPolicy.CamelCase.ConvertName(op.Name);
            });

            operation.Tags?.ToList()?.ForEach(tag =>
            {
                tag.Name = tag.Name;
            });
        }
    }
}
