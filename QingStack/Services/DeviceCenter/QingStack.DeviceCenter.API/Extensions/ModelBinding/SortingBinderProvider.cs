/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SortingBinderProvider.cs
    文件功能描述：排序解释器模型绑定提供者


    创建标识：QingRain - 20211115

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QingStack.DeviceCenter.Application.Services.Generics;
using System;
using System.Collections.Generic;

namespace QingStack.DeviceCenter.API.Extensions.ModelBinding
{
    public class SortingBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(IEnumerable<SortingDescriptor>))
            {
                return new SortingModelBinder();
            }

            return null;
        }
    }
}
