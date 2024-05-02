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

    modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
    modelBuilder.Entity<User>().HasKey(u => u. UserId);


        modelBuilder.Entity<Product>()
        .HasKey(oi => oi.ProductId); 
                
        modelBuilder.Entity<User>()
        .HasKey(oi => oi.UserId); 
                
        modelBuilder.Entity<Order>()
        .HasKey(oi => oi.OrderId);

}}
 