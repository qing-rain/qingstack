/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectEntityTypeConfiguration.cs
    文件功能描述：项目实体映射配置


    创建标识：QingRain - 20211109
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Projects
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
        }
    }
}
