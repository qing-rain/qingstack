/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceCenterDbContext.cs
    文件功能描述：设备中心上下文


    创建标识：QingRain - 20211109

    
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using QingStack.DeviceCenter.Domain.UnitOfWork;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.EntityFrameworks
{
    public class DeviceCenterDbContext : DbContext, IUnitOfWork
    {
        public DeviceCenterDbContext(DbContextOptions<DeviceCenterDbContext> options) : base(options) { }
        /// <summary>
        /// 隐式实现
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        async Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => await base.SaveChangesAsync(cancellationToken);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ///扫描当前执行程序集所有配置
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
