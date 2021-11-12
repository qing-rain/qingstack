/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IPermissionGrantRepository.cs
    文件功能描述：权限授予自定义仓储

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate
{
    /// <summary>
    /// 自定义权限仓储
    /// </summary>
    public interface IPermissionGrantRepository : IRepository<PermissionGrant, Guid>
    {
        /// <summary>
        /// 查询权限是否通过
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PermissionGrant> FindAsync(string name, string providerName, string providerKey, CancellationToken cancellationToken = default);
        /// <summary>
        /// 查询权限记录
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<PermissionGrant>> GetListAsync(string providerName, string providerKey, CancellationToken cancellationToken = default);
        /// <summary>
        /// 查询权限记录
        /// </summary>
        /// <param name="names"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<PermissionGrant>> GetListAsync(string[] names, string providerName, string providerKey, CancellationToken cancellationToken = default);
    }
}
