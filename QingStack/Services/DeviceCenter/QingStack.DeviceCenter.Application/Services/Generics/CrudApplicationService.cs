/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：CrudApplicationService.cs
    文件功能描述：唯一主键通用增删查改应用服务


    创建标识：QingRain - 20211111

 ----------------------------------------------------------------*/
using AutoMapper;
using QingStack.DeviceCenter.Domain.Entities;
using QingStack.DeviceCenter.Domain.Repositories;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Services.Generics
{
    /// <summary>
    /// 可细化扩展，优化泛型实体，二次继承，简便调用实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetResponseModel"></typeparam>
    /// <typeparam name="TGetListRequestModel"></typeparam>
    /// <typeparam name="TGetListResponseModel"></typeparam>
    /// <typeparam name="TCreateRequestModel"></typeparam>
    /// <typeparam name="TUpdateRequestModel"></typeparam>
    public class CrudApplicationService<TEntity, TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel> : AlternateKeyCrudApplicationService<TEntity, TKey, TGetResponseModel, TGetListRequestModel, TGetListResponseModel, TCreateRequestModel, TUpdateRequestModel> where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// 重写父类仓储
        /// </summary>
        protected new IRepository<TEntity, TKey> Repository { get; }

        public CrudApplicationService(IRepository<TEntity, TKey> repository, IMapper mapper) : base(repository, mapper) => Repository = repository;

        protected async override Task DeleteByIdAsync(TKey id) => await Repository.DeleteAsync(id, true);

        protected async override Task<TEntity> GetEntityByIdAsync(TKey id) => await Repository.GetAsync(id);
    }
}
