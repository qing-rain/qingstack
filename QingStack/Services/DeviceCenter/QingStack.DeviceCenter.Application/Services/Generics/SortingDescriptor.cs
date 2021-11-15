/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：SortingDescriptor.cs
    文件功能描述：排序解释器


    创建标识：QingRain - 20211115
 ----------------------------------------------------------------*/
using System.Diagnostics.CodeAnalysis;

namespace QingStack.DeviceCenter.Application.Services.Generics
{
    public enum SortingOrder { Ascending, Descending }

    public class SortingDescriptor
    {
        [AllowNull]
        public string PropertyName { get; set; }

        public SortingOrder SortDirection { get; set; }
    }
}
