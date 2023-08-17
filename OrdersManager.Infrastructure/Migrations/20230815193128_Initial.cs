using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrdersManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "OrdersItem",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrdersItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "CustomerName", "OrderDate", "OrderNumber" },
                values: new object[,]
                {
                    { new Guid("0e3351e5-9327-4cb4-ae31-ff2e41d2b86a"), "Ali", new DateTime(2023, 8, 15, 23, 31, 28, 40, DateTimeKind.Local).AddTicks(5462), "Order_2023_1" },
                    { new Guid("22d016d9-1429-46a7-bf67-f049f8d8c247"), "Alex", new DateTime(2023, 8, 15, 23, 31, 28, 40, DateTimeKind.Local).AddTicks(5465), "Order_2023_2" }
                });

            migrationBuilder.InsertData(
                table: "OrdersItem",
                columns: new[] { "OrderItemId", "OrderId", "ProductName", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("1067a04d-44cf-4da1-9110-bc5ae046fe66"), new Guid("0e3351e5-9327-4cb4-ae31-ff2e41d2b86a"), "Product2", 3L, 10L },
                    { new Guid("185ad5e1-3618-4c7b-ac8f-00e6f6193a65"), new Guid("22d016d9-1429-46a7-bf67-f049f8d8c247"), "Product3", 2L, 50L },
                    { new Guid("813a857d-5331-4299-a3cf-3a6bf7c7932d"), new Guid("22d016d9-1429-46a7-bf67-f049f8d8c247"), "Product4", 1L, 50L },
                    { new Guid("96a9d8ff-87f9-4a29-ab22-5e58afed1248"), new Guid("0e3351e5-9327-4cb4-ae31-ff2e41d2b86a"), "Product1", 5L, 20L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItem_OrderId",
                table: "OrdersItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersItem");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
