/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceTagEntityTypeConfiguration.cs
    文件功能描述：设备标签实体映射配置


    创建标识：QingRain - 20211109
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Products
{
    public class DeviceTagEntityTypeConfiguration : IEntityTypeConfiguration<DeviceTag>
    {
        public void Configure(EntityTypeBuilder<DeviceTag> builder)
        {
            builder.ToTable("DeviceTags", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
        }
    }
}
