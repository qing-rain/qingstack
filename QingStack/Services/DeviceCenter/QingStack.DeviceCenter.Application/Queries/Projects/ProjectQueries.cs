/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectQueries.cs
    文件功能描述：基于Dapper库的项目查询


    创建标识：QingRain - 20211114
 ----------------------------------------------------------------*/
using Dapper;
using QingStack.DeviceCenter.Application.Models.Generics;
using QingStack.DeviceCenter.Application.Models.Projects;
using QingStack.DeviceCenter.Application.Queries.Factories;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QingStack.DeviceCenter.Application.Queries.Projects
{
    public class ProjectQueries : IProjectQueries
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public ProjectQueries(IDbConnectionFactory dbConnectionFactory) => _dbConnectionFactory = dbConnectionFactory;
        public async Task<ProjectGetResponseModel> GetProjectAsync(int id)
        {
            using var connection = await _dbConnectionFactory.CreateConnection();
            string sql = "SELECT * FROM Projects WHERE Id=@Id LIMIT 1";
            var result = await connection.QueryFirstAsync<ProjectGetResponseModel>(sql, new { id });
            return result;
        }

        public async Task<PagedResponseModel<ProjectGetResponseModel>> GetProjectsAsync(ProjectPagedRequestModel model)
        {
            using IDbConnection connection = await _dbConnectionFactory.CreateConnection();
            string listSql = $"SELECT * FROM Projects WHERE Name LIKE @Keyword ORDER BY ID limit " + (model.PageNumber - 1) * model.PageSize + " , @PageSize;";
            string countSql = $"SELECT COUNT(*) FROM Projects WHERE Name LIKE @Keyword";

            model.Keyword = $"%{model.Keyword}%";

            using var gridReader = await connection.QueryMultipleAsync(listSql + countSql, model);

            var list = await gridReader.ReadAsync<ProjectGetResponseModel>();
            int count = await gridReader.ReadSingleAsync<int>();
            list ??= Enumerable.Empty<ProjectGetResponseModel>();

            return new PagedResponseModel<ProjectGetResponseModel>(list.ToList(), count);
        }
    }
}
