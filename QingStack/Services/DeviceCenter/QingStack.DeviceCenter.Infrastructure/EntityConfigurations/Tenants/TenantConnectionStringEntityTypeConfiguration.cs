/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：TenantConnectionStringEntityTypeConfiguration.cs
    文件功能描述：租户连接字符串实体映射配置


    创建标识：QingRain - 20211110

 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
{
    public class TenantConnectionStringEntityTypeConfiguration : IEntityTypeConfiguration<TenantConnectionString>
    {
        public void Configure(EntityTypeBuilder<TenantConnectionString> builder)
        {
            builder.ToTable("TenantConnectionStrings", Constants.DbConstants.DefaultTableSchema);

            builder.HasKey(x => new { x.TenantId, x.Name });

            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
            builder.Property(e => e.Value).IsRequired().HasMaxLength(1024);
        }
    }

}
