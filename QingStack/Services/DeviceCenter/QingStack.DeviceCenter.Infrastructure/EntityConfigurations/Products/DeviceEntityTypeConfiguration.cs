/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：DeviceEntityTypeConfiguration.cs
    文件功能描述：设备实体映射配置


    创建标识：QingRain - 20211109
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Products
{
    public class DeviceEntityTypeConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(20);
            builder.Property(e => e.SerialNo).IsRequired().HasMaxLength(36);

            ///值转换器
            var converter = new ValueConverter<GeoCoordinate, string>(v => v, v => (GeoCoordinate)v);
            builder.Property(e => e.Coordinate).HasConversion(converter);

            //存储至表DeviceAddresses
            builder.OwnsOne(e => e.Address, da => da.ToTable("DeviceAddresses"));
        }
    }
}
