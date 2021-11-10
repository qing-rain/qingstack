using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QingStack.DeviceCenter.Domain.Aggregates.TenantAggregate;

namespace QingStack.DeviceCenter.Infrastructure.EntityConfigurations.Tenants
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants", Constants.DbConstants.DefaultTableSchema);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(64);
            builder.HasMany(e => e.ConnectionStrings).WithOne().HasForeignKey(e => e.TenantId).IsRequired();

            builder.HasIndex(u => u.Name);
        }
    }

}
