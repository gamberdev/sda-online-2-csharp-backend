using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Tables;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.EF;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet <Product> Products {get;set;}
    public DbSet <Order> Orders {get;set;}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

             // Users Constraint
              modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemId);
              modelBuilder.Entity<OrderItem>(). Property(oi => oi. Quantity);
              modelBuilder.Entity<OrderItem>(). Property(oi => oi. Price);

             // Category Id PK 
            modelBuilder.Entity<Category>().HasKey(category => category.CategoryId);    
            modelBuilder.Entity<Category>().Property(category => category.CategoryId).IsRequired().ValueGeneratedOnAdd();

            // Category Name
            modelBuilder.Entity<Category>().Property(category => category.Name).IsRequired();
            modelBuilder.Entity<Category>().HasIndex(category => category.Name).IsUnique();

            // Category Slug
            modelBuilder.Entity<Category>().HasIndex(category => category.Slug).IsUnique();

            // Product Id product PK 
            modelBuilder.Entity<Product>().HasKey(product => product.ProductId);    
            modelBuilder.Entity<Product>().Property(product => product.ProductId).IsRequired().ValueGeneratedOnAdd();

            // Product Name
            modelBuilder.Entity<Product>().Property(product => product.Name).IsRequired();
            modelBuilder.Entity<Product>().HasIndex(product => product.Name).IsUnique();

            // Product Price
            modelBuilder.Entity<Product>().Property(product => product.Price).IsRequired();

            // Product Slug
            modelBuilder.Entity<Product>().Property(product => product.Slug);

            // Product Description
            modelBuilder.Entity<Product>().Property(product => product.Description);

            // Product Image
            modelBuilder.Entity<Product>().Property(product => product.Image);




           // Relationship 
         modelBuilder.Entity<OrderItem>()
        .HasOne(oi => oi.Product)
        .WithMany()
        .HasForeignKey(oi => oi.ProductId)
         .IsRequired(false);

        modelBuilder.Entity<OrderItem>()
         .HasOne(oi => oi.User)
        .WithMany()
        .HasForeignKey(oi => oi.UserId)
         .OnDelete(DeleteBehavior.Restrict);

       modelBuilder.Entity<OrderItem>()
        .HasOne(oi => oi.Order)
        .WithMany()
         .HasForeignKey(oi => oi.OrderId)
         .IsRequired(false);

       modelBuilder.Entity<Product>()
       .HasKey(p => p.ProductId);
       modelBuilder.Entity<User>()
       .HasKey(u => u. UserId);

        modelBuilder.Entity<Product>()
        .HasKey(oi => oi.ProductId); 
                
        modelBuilder.Entity<User>()
        .HasKey(oi => oi.UserId); 
                
        modelBuilder.Entity<Order>()
        .HasKey(oi => oi.OrderId);

         // Configure one-to-many relationship between Product and Category
                modelBuilder.Entity<Product>()
               .HasOne(p=> p.Category)           // Each product belongs to one category
               .WithMany(c => c.Product)         // Each category can have many products
               .HasForeignKey(p => p.CategoryId) // Foreign key property in the Product entity
               .IsRequired();     // CategoryId is required in Product entity

}}
 