using Enilton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enilton.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(oi => oi.UnitPrice).IsRequired();
            builder.Property(oi => oi.TotalPrice).IsRequired();
            builder.HasOne<Order>().WithMany().HasForeignKey(oi => oi.OrderId);
        }
    }
}
