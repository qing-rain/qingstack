/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ProductEntityTypeConfiguration.cs
    文件功能描述：产品实体映射配置


    创建标识：QingRain - 20211109
 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.ProductAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Products
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            //忽略领域事件
            builder.Ignore(e => e.DomainEvents);
            builder.Property(e => e.Name).HasMaxLength(20);
            builder.Property(e => e.Remark).HasMaxLength(100);
        }
    }
}
