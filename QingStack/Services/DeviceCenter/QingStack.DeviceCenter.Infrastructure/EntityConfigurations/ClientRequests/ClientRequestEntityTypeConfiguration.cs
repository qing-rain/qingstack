/*----------------------------------------------------------------
    Copyright (C) 2021 QingRain

    文件名：ClientRequestEntityTypeConfiguration.cs
    文件功能描述：幂等性实体映射配置


    创建标识：QingRain - 20211114

 ----------------------------------------------------------------*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Infrastructure.Idempotency;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.ClientRequests
{
    public class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> builder)
        {
            builder.ToTable("Idempotency", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Name).IsRequired();
            builder.Property(cr => cr.Time).IsRequired();
        }
    }
}
