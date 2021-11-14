/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IConnectionStringProvider.cs
    文件功能描述：连接字符串提供者


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Infrastructure.ConnectionStrings
{
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// 连接字符串名称 为空则共享库
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        Task<string> GetAsync(string? connectionStringName = null);
    }
}
