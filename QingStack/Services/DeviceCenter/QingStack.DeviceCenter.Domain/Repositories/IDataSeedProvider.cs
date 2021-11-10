/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IDataSeedProvider.cs
    文件功能描述：演示数据提供者


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using System;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Repositories
{
    public interface IDataSeedProvider
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
