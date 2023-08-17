﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrdersManager.Infrastructure.DatabaseContext;

#nullable disable

namespace OrdersManager.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230815193128_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrdersManager.Core.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderID = new Guid("0e3351e5-9327-4cb4-ae31-ff2e41d2b86a"),
                            CustomerName = "Ali",
                            OrderDate = new DateTime(2023, 8, 15, 23, 31, 28, 40, DateTimeKind.Local).AddTicks(5462),
                            OrderNumber = "Order_2023_1"
                        },
                        new
                        {
                            OrderID = new Guid("22d016d9-1429-46a7-bf67-f049f8d8c247"),
                            CustomerName = "Alex",
                            OrderDate = new DateTime(2023, 8, 15, 23, 31, 28, 40, DateTimeKind.Local).AddTicks(5465),
                            OrderNumber = "Order_2023_2"
                        });
                });

            modelBuilder.Entity("OrdersManager.Core.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<long>("UnitPrice")
                        .HasColumnType("bigint");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersItem");

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("96a9d8ff-87f9-4a29-ab22-5e58afed1248"),
                            OrderId = new Guid("0e3351e5-9327-4cb4-ae31-ff2e41d2b86a"),
                            ProductName = "Product1",
                            Quantity = 5L,
                            UnitPrice = 20L
                        },
                        new
                        {
                            OrderItemId = new Guid("1067a04d-44cf-4da1-9110-bc5ae046fe66"),
                            OrderId = new Guid("0e3351e5-9327-4cb4-ae31-ff2e41d2b86a"),
                            ProductName = "Product2",
                            Quantity = 3L,
                            UnitPrice = 10L
                        },
                        new
                        {
                            OrderItemId = new Guid("185ad5e1-3618-4c7b-ac8f-00e6f6193a65"),
                            OrderId = new Guid("22d016d9-1429-46a7-bf67-f049f8d8c247"),
                            ProductName = "Product3",
                            Quantity = 2L,
                            UnitPrice = 50L
                        },
                        new
                        {
                            OrderItemId = new Guid("813a857d-5331-4299-a3cf-3a6bf7c7932d"),
                            OrderId = new Guid("22d016d9-1429-46a7-bf67-f049f8d8c247"),
                            ProductName = "Product4",
                            Quantity = 1L,
                            UnitPrice = 50L
                        });
                });

            modelBuilder.Entity("OrdersManager.Core.Entities.OrderItem", b =>
                {
                    b.HasOne("OrdersManager.Core.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OrdersManager.Core.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}