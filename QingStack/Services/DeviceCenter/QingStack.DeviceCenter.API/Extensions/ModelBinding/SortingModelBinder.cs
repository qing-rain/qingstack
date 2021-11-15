/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SortingModelBinder.cs
    文件功能描述：排序解释器模型绑定


    创建标识：QingRain - 20211115

 ----------------------------------------------------------------*/
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QingStack.DeviceCenter.Application.Services.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.API.Extensions.ModelBinding
{
    public class SortingModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext = bindingContext ?? throw new ArgumentNullException(nameof(bindingContext));

            string modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            string? value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            var sorter = JsonSerializer.Deserialize<IDictionary<string, string>>(value);

            var sortingDirectionMap = new Dictionary<string, SortingOrder>
            {
                { "ascend", SortingOrder.Ascending },
                { "descend", SortingOrder.Descending }
            };

            if (sorter is not null)
            {
                var effectSorter = sorter.Where(item => sortingDirectionMap.ContainsKey(item.Value));
                var sorting = effectSorter.Select(item => new SortingDescriptor
                {
                    PropertyName = item.Key,
                    SortDirection = sortingDirectionMap[item.Value]
                });

                bindingContext.Result = ModelBindingResult.Success(sorting);
            }

            return Task.CompletedTask;
        }
    }
}
