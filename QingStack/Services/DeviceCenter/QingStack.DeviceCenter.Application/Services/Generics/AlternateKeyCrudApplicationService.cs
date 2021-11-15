/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：AlternateKeyCrudApplicationService.cs
    文件功能描述：联合主键通用增删查改应用服务


    创建标识：QingRain - 20211111

    修改标识：QingRain - 20211115
    修改描述：调整排序方法
 ----------------------------------------------------------------*/
using AutoMapper;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Domain.Entities;
using QingStack.DeviceCenter.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Generics
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">实体聚合根</typeparam>
    /// <typeparam name="TKey">主键</typeparam>
    /// <typeparam name="TGetResponseModel">返回模型</typeparam>
    /// <typeparam name="TGetListRequestModel">查询集合请求模型</typeparam>
    /// <typeparam name="TGetListResponseModel">查询集合返回模型</typeparam>
    /// <typeparam name="TCreateRequestModel">创建模型</typeparam>
    /// <typeparam name="TUpdateRequestModel">更新模型</typeparam>
    public abstract class AlternateKeyCrudApplicationService<TEntity, TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel> : ICrudApplicationService<TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel> where TEntity : BaseEntity
    {
        protected IRepository<TEntity> Repository { get; }

        private readonly IMapper _mapper;

        public AlternateKeyCrudApplicationService(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TGetResponseModel> CreateAsync(TCreateRequestModel requestModel)
        {
            TEntity entity = _mapper.Map<TEntity>(requestModel);
            await Repository.InsertAsync(entity, true);
            return _mapper.Map<TGetResponseModel>(entity);
        }

        public virtual async Task DeleteAsync(TKey id) => await DeleteByIdAsync(id);

        /// <summary>
        /// 抽象给外部实现删除方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract Task DeleteByIdAsync(TKey id);

        public virtual async Task<TGetResponseModel> GetAsync(TKey id) => _mapper.Map<TGetResponseModel>(await GetEntityByIdAsync(id));
        /// <summary>
        /// 抽象给外部实现查询方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract Task<TEntity> GetEntityByIdAsync(TKey id);

        public virtual async Task<PagedResponseModel<TGetListResponseModel>> GetListAsync(TGetListRequestModel requestModel)
        {
            IQueryable<TEntity> query = CreateFilteredQuery(requestModel);

            int totalCount = await Repository.AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, requestModel);
            query = ApplyPaging(query, requestModel);

            var entities = await Repository.AsyncExecuter.ToListAsync(query);
            var entityDtos = _mapper.Map<List<TGetListResponseModel>>(entities);

            return new PagedResponseModel<TGetListResponseModel>(entityDtos, totalCount);
        }
        /// <summary>
        /// 默认查询集合
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>

        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetListRequestModel requestModel) => Repository.Query;

        /// <summary>
        /// 默认排序
        /// </summary>
        /// <param name="query"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListRequestModel requestModel)
        {
            var pagedRequestModel = requestModel as PagedRequestModel;

            if (pagedRequestModel is not null && pagedRequestModel.Sorter is not null && pagedRequestModel.Sorter.Any())
            {
                //获取泛型的实体的所有参数
                var properties = query.GetType().GetGenericArguments().First().GetProperties();
                IOrderedQueryable<TEntity>? orderedQueryable = null;
                foreach (SortingDescriptor sortingDescriptor in pagedRequestModel.Sorter)
                {
                    string? propertyName = properties.SingleOrDefault(p => string.Equals(p.Name, sortingDescriptor.PropertyName, StringComparison.OrdinalIgnoreCase))?.Name;

                    if (propertyName is null)
                    {
                        throw new KeyNotFoundException(sortingDescriptor.PropertyName);
                    }

                    if (sortingDescriptor.SortDirection == SortingOrder.Ascending)
                    {
                        orderedQueryable = orderedQueryable is null ? query.OrderBy(propertyName) : orderedQueryable.ThenBy(propertyName);
                    }
                    else if (sortingDescriptor.SortDirection == SortingOrder.Descending)
                    {
                        orderedQueryable = orderedQueryable is null ? query.OrderByDescending(propertyName) : orderedQueryable.ThenByDescending(propertyName);
                    }
                }

                return orderedQueryable ?? query;
            }
            string firstPropertyName = typeof(TEntity).GetProperties().First().Name;

            return query.OrderByDescending(firstPropertyName);
        }
        /// <summary>
        /// 默认分页
        /// </summary>
        /// <param name="query"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>

        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListRequestModel requestModel)
        {
            if (requestModel is PagedRequestModel model)
            {
                return query.Skip((model.PageNumber - 1) * model.PageSize).Take(model.PageSize);
            }

            return query;
        }

        public async Task<TGetResponseModel> UpdateAsync(TUpdateRequestModel requestModel)
        {
            TEntity entity = _mapper.Map<TEntity>(requestModel);
            await Repository.UpdateAsync(entity, true);
            return _mapper.Map<TGetResponseModel>(entity);
        }
    }
}
