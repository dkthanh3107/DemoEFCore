using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContosoPizza.Models;

namespace ConsotoPizza.Data
{
    public partial class ContosoPizzaContext : DbContext
    {
        public ContosoPizzaContext()
        {
        }

        public ContosoPizzaContext(DbContextOptions<ContosoPizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasIndex(e => e.OrderId1, "IX_OrderDetails_OrderId1");

                entity.HasIndex(e => e.ProductId1, "IX_OrderDetails_ProductId1");

                entity.HasOne(d => d.OrderId1Navigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId1);

                entity.HasOne(d => d.ProductId1Navigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId1);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
