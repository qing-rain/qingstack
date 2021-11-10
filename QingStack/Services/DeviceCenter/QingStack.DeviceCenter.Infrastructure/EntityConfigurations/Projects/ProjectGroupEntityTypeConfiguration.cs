/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProjectGroupEntityTypeConfiguration.cs
    文件功能描述：项目组实体映射配置


    创建标识：QingRain - 20211109
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.ProjectAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Projects
{
    public class ProjectGroupEntityTypeConfiguration : IEntityTypeConfiguration<ProjectGroup>
    {
        public void Configure(EntityTypeBuilder<ProjectGroup> builder)
        {
            builder.ToTable("ProjectGroups", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Remark).HasMaxLength(100);
        }
    }
}
