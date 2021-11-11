/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CustomMvcBuilderExtensions.cs
    文件功能描述：自定义MVCBuilder扩展容器


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using FluentValidation.AspNetCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomExtensions(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())).AddDataAnnotationsLocalization();

            return builder;
        }
    }
}
