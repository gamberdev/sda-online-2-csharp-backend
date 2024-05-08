﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ecommerce.EntityFramework;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240508111759_testauth")]
    partial class testauth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("slug");

                    b.HasKey("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("category");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("delivery_address");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delivery_date");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("order_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer")
                        .HasColumnName("order_status");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("payment_method");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("total_price");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("orderItem_id");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("order_item");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("slug");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Description");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("products");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("review_id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("review");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsBanned")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_banned");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Order", b =>
                {
                    b.HasOne("ecommerce.EntityFramework.Table.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.OrderItem", b =>
                {
                    b.HasOne("ecommerce.EntityFramework.Table.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ecommerce.EntityFramework.Table.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecommerce.EntityFramework.Table.User", "User")
                        .WithMany("OrderItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Product", b =>
                {
                    b.HasOne("ecommerce.EntityFramework.Table.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Review", b =>
                {
                    b.HasOne("ecommerce.EntityFramework.Table.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ecommerce.EntityFramework.Table.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Category", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.Product", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("ecommerce.EntityFramework.Table.User", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}