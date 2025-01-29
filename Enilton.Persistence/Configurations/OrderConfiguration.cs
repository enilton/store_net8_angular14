﻿using Enilton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enilton.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired();
            builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId);
        }
    }
}
