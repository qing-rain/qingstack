/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：PermissionGrantEntityTypeConfiguration.cs
    文件功能描述：权限实体映射配置

    创建标识：QingRain - 20211111
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.PermissionAggregate;
using System.Linq;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Permissions
{
    /// <summary>
    /// 权限字段配置
    /// </summary>
    public class PermissionGrantEntityTypeConfiguration : IEntityTypeConfiguration<PermissionGrant>
    {
        public void Configure(EntityTypeBuilder<PermissionGrant> builder)
        {
            //权限表
            builder.ToTable("PermissionGrants", Constants.DbConstants.DefaultTableSchema);

            builder.HasKey(e => e.Id);

            //变量循环设置字符串类型属性长度为20，且必填
            foreach (IMutableEntityType entityType in builder.Metadata.Model.GetEntityTypes())
            {
                if (entityType.ClrType == typeof(PermissionGrant))
                {
                    foreach (IMutableProperty property in entityType.GetProperties().Where(p => p.ClrType == typeof(string)))
                    {
                        property.SetMaxLength(20);
                        builder.Property(property.Name).IsRequired(false);
                    }
                }
            }

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
            //设置联合索引
            builder.HasIndex(e => new { e.Name, e.ProviderName, e.ProviderKey });
        }
    }
}
