using Enilton.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enilton.Persistence.Context
{
    public class SqlServerDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.CustomerId)
                    .IsRequired();

                entity.Property(o => o.OrderDate)
                    .IsRequired();

                entity.Property(o => o.TotalAmount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(o => o.Status)
                    .IsRequired();
            });

            // Configuração da entidade OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.OrderId)
                    .IsRequired();

                entity.Property(oi => oi.ProductId)
                    .IsRequired();

                entity.Property(oi => oi.ProductName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(oi => oi.Quantity)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(oi => oi.UnitPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(oi => oi.TotalPrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // Relacionamento com Order
                entity.HasOne<Order>()
                    .WithMany()
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
