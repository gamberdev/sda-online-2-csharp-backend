using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.EntityFramework.Table;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.EntityFramework;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //PK for all entity
        modelBuilder.Entity<Product>().HasKey(product => product.ProductId);
        modelBuilder.Entity<User>().HasKey(user => user.UserId);
        modelBuilder.Entity<Order>().HasKey(order => order.OrderId);
        modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemId);
        modelBuilder.Entity<Category>().HasKey(category => category.CategoryId);
        modelBuilder.Entity<Review>().HasKey(review => review.ReviewId);

        // Category Constraint
        // Category Name
        modelBuilder.Entity<Category>().Property(category => category.Name).IsRequired();
        modelBuilder.Entity<Category>().HasIndex(category => category.Name).IsUnique();
        // Category Slug
        modelBuilder.Entity<Category>().HasIndex(category => category.Slug).IsUnique();

        // Product Constraint
        // Product Name
        modelBuilder.Entity<Product>().Property(product => product.Name).IsRequired();
        modelBuilder.Entity<Product>().HasIndex(product => product.Name).IsUnique();
        // Product Price
        modelBuilder.Entity<Product>().Property(product => product.Price).IsRequired();

        // Users Constraint
        modelBuilder.Entity<User>().Property(u => u.FullName).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Phone).IsRequired();
        modelBuilder.Entity<User>().HasIndex(u => u.Phone).IsUnique();
        modelBuilder
            .Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<User>().Property(u => u.IsBanned).HasDefaultValue(false);

        // Review Constraint
        modelBuilder.Entity<Review>().Property(r => r.Comment).IsRequired();

        // Order Constraint
        modelBuilder.Entity<Order>().Property(u => u.DeliveryAddress).IsRequired();
        modelBuilder
            .Entity<Order>()
            .Property(o => o.OrderDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // OrderItem Constraint
        modelBuilder.Entity<OrderItem>().Property(oi => oi.Quantity).IsRequired();

        // Relationship
        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(oi => oi.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .IsRequired();

        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.User)
            .WithMany(oi => oi.OrderItems)
            .HasForeignKey(oi => oi.UserId)
            .IsRequired();

        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(oi => oi.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired(false);

        // Configure one-to-many relationship between Product and Category
        modelBuilder
            .Entity<Product>()
            .HasOne(p => p.Category) // Each product belongs to one category
            .WithMany(c => c.Product) // Each category can have many products
            .HasForeignKey(p => p.CategoryId) // Foreign key property in the Product entity
            .IsRequired(false); // CategoryId is required in Product entity

        //Relationship User & Review
        modelBuilder
            .Entity<User>()
            .HasMany(r => r.Reviews)
            .WithOne(u => u.User)
            .HasForeignKey(r => r.UserId);

        //Relationship Product & Review
        modelBuilder
            .Entity<Product>()
            .HasMany(r => r.Reviews)
            .WithOne(u => u.Product)
            .HasForeignKey(r => r.ProductId);

        // Relationship User & order
        modelBuilder
            .Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
    }
}
