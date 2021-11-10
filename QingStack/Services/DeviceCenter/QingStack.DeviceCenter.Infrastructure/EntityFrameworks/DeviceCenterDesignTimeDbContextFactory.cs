/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceCenterDbContext.cs
    文件功能描述：设备中心设计时DbContext工厂


    创建标识：QingRain - 20211110
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeviceCenterDbContext>
    {
        public DeviceCenterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeviceCenterDbContext>();
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
            optionsBuilder.UseMySql(@"Server=localhost;Port=3306;Database=DeviceCenter;Uid=root;Pwd=123321;", serverVersion);

            return new DeviceCenterDbContext(optionsBuilder.Options);
        }
    }
}
