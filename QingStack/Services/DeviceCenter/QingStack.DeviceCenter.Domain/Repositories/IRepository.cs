/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：IRepository.cs
    文件功能描述：仓储接口


    创建标识：QingRain - 20211108

    修改标识：QingRain - 20211108
    修改描述：仓储模式中支持规约查询

 ----------------------------------------------------------------*/
using QingStack.DeviceCenter.Domain.Entities;
using QingStack.DeviceCenter.Domain.Specifications;
using QingStack.DeviceCenter.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave">是否立即保存数据库</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除指定条件实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="includeDetails">是否查询外键关联实体</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询表记录
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sorting">l排序表达式</param>
        /// <param name="includeDetails">是否查询外键关联实体</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, object>> sorting, bool includeDetails = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据表达式获取详情数据
        /// </summary>
        IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors);

        /// <summary>
        /// 查询满足条件的第一条数据
        /// </summary>
        /// <param name="predicate">查询表达式</param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity?> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询满足条件的数据 未查到返回异常
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// 规约查询集合
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// 规约查询集合
        /// </summary>
        /// <typeparam name="TResult">指定实体输出</typeparam>
        /// <param name="specification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TResult>> GetListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
        /// <summary>
        /// 规约查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="specification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TResult> GetAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);


        /// <summary>
        /// 任意查询
        /// </summary>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }
        /// <summary>
        /// 异步执行器
        /// </summary>
        IAsyncQueryableProvider AsyncExecuter { get; }
    }
    /// <summary>
    /// 带主键
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : BaseEntity<TKey>
    {
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);

        Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default);
    }

}
