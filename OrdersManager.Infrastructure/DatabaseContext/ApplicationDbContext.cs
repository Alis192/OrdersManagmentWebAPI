using Microsoft.EntityFrameworkCore;
using OrdersManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext() { }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrdersItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Guid firstOrderID = Guid.Parse("0E3351E5-9327-4CB4-AE31-FF2E41D2B86A");
            Guid secondOrderID = Guid.Parse("22D016D9-1429-46A7-BF67-F049F8D8C247");

            modelBuilder.Entity<Order>().HasData(new Order()
            {
                OrderDate = DateTime.Now,
                OrderID = firstOrderID,
                OrderNumber = "Order_2023_1",
                CustomerName = "Ali",
            },
            new Order()
            {
                OrderDate = DateTime.Now,
                OrderID = secondOrderID,
                OrderNumber = "Order_2023_2",
                CustomerName = "Alex",
            });

            modelBuilder.Entity<OrderItem>().HasData(
               new OrderItem()
               {
                   OrderItemId = Guid.NewGuid(),
                   OrderId = firstOrderID,
                   ProductName = "Product1",
                   Quantity = 5,
                   UnitPrice = 20
               },
               new OrderItem()
               {
                   OrderItemId = Guid.NewGuid(),
                   OrderId = firstOrderID,
                   ProductName = "Product2",
                   Quantity = 3,
                   UnitPrice = 10
               },
               new OrderItem()
               {
                   OrderItemId = Guid.NewGuid(),
                   OrderId = secondOrderID,
                   ProductName = "Product3",
                   Quantity = 2,
                   UnitPrice = 50
               },
               new OrderItem()
               {
                   OrderItemId = Guid.NewGuid(),
                   OrderId = secondOrderID,
                   ProductName = "Product4",
                   Quantity = 1,
                   UnitPrice = 50
               }
           );


        }
    }
}
