using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GradDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    Multiplier = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSize_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSize_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CashierId = table.Column<int>(type: "int", nullable: false),
                    CaptainId = table.Column<int>(type: "int", nullable: false),
                    WaiterId = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CaptainId",
                        column: x => x.CaptainId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CashierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CancelledOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CancelledById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelledOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CancelledOrders_Users_CancelledById",
                        column: x => x.CancelledById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnedOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnedOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnedOrders_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnedOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Hot Coffee", "Rich chocolate flavor with espresso and steamed milk", "https://cdn.grad-demo.com/items/mocha-flavor.jpg", true, "Mocha Flavor", 35.0 },
                    { 2, "Hot Coffee", "Smooth espresso with vanilla syrup and steamed milk", "https://cdn.grad-demo.com/items/vanilla-latte.jpg", true, "Vanilla Latte", 32.0 },
                    { 3, "Hot Coffee", "Creamy latte with rich hazelnut flavor", "https://cdn.grad-demo.com/items/hazelnut-latte.jpg", true, "Hazelnut Latte", 34.0 },
                    { 4, "Hot Coffee", "Espresso with vanilla syrup, milk, and caramel drizzle", "https://cdn.grad-demo.com/items/caramel-macchiato.jpg", true, "Caramel Macchiato", 38.0 },
                    { 5, "Cold Coffee", "Chilled brewed coffee served over ice", "https://cdn.grad-demo.com/items/cold-coffee.jpg", true, "Cold Coffee", 25.0 },
                    { 6, "Cold Coffee", "Refreshing cold latte with strawberry syrup", "https://cdn.grad-demo.com/items/strawberry-latte.jpg", true, "Strawberry Latte", 36.0 },
                    { 7, "Cold Coffee", "Espresso shots chilled and served over ice", "https://cdn.grad-demo.com/items/iced-americano.jpg", true, "Iced Americano", 28.0 },
                    { 8, "Cold Coffee", "Smooth cold brewed coffee served chilled", "https://cdn.grad-demo.com/items/cold-brew.jpg", true, "Cold Brew", 30.0 },
                    { 9, "Tea", "Traditional green tea with fresh mint leaves", "https://cdn.grad-demo.com/items/moroccan-mint-tea.jpg", true, "Moroccan Mint Tea", 20.0 },
                    { 10, "Tea", "Classic black tea with bergamot orange flavor", "https://cdn.grad-demo.com/items/earl-grey.jpg", true, "Earl Grey", 18.0 },
                    { 11, "Tea", "Spiced tea with steamed milk and honey", "https://cdn.grad-demo.com/items/chai-latte.jpg", true, "Chai Latte", 26.0 },
                    { 12, "Pastries", "Freshly baked butter croissant", "https://cdn.grad-demo.com/items/croissant.jpg", true, "Croissant", 15.0 },
                    { 13, "Pastries", "Rich chocolate muffin with chocolate chips", "https://cdn.grad-demo.com/items/chocolate-muffin.jpg", true, "Chocolate Muffin", 18.0 },
                    { 14, "Pastries", "Traditional scone with fresh blueberries", "https://cdn.grad-demo.com/items/blueberry-scone.jpg", true, "Blueberry Scone", 16.0 },
                    { 15, "Pastries", "Soft cinnamon roll with cream cheese frosting", "https://cdn.grad-demo.com/items/cinnamon-roll.jpg", true, "Cinnamon Roll", 22.0 },
                    { 16, "Special Drinks", "Rich and creamy hot chocolate", "https://cdn.grad-demo.com/items/hot-chocolate.jpg", true, "Hot Chocolate", 24.0 },
                    { 17, "Special Drinks", "Green tea powder with steamed milk", "https://cdn.grad-demo.com/items/matcha-latte.jpg", true, "Matcha Latte", 32.0 },
                    { 18, "Special Drinks", "Traditional strong Turkish coffee", "https://cdn.grad-demo.com/items/turkish-coffee.jpg", true, "Turkish Coffee", 20.0 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Cashier" },
                    { 3, "Captain" },
                    { 4, "Waiter" },
                    { 5, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "S", "Small (8oz)" },
                    { 2, "M", "Medium (12oz)" },
                    { 3, "L", "Large (16oz)" },
                    { 4, "X", "Extra Large (20oz)" }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Area" },
                values: new object[,]
                {
                    { 1, "Main Hall - Window View" },
                    { 2, "Main Hall - Center" },
                    { 3, "Main Hall - Quiet Corner" },
                    { 4, "Outdoor Terrace - Garden" },
                    { 5, "Outdoor Terrace - Street View" },
                    { 6, "VIP Lounge - Private" },
                    { 7, "VIP Lounge - Family" },
                    { 8, "Smoking Area - Outdoor" },
                    { 9, "Reading Corner" },
                    { 10, "Business Meeting Area" }
                });

            migrationBuilder.InsertData(
                table: "ItemSize",
                columns: new[] { "Id", "ItemId", "Multiplier", "SizeId" },
                values: new object[,]
                {
                    { 1, 1, 0.80000000000000004, 1 },
                    { 2, 1, 1.0, 2 },
                    { 3, 1, 1.2, 3 },
                    { 4, 1, 1.5, 4 },
                    { 5, 2, 0.80000000000000004, 1 },
                    { 6, 2, 1.0, 2 },
                    { 7, 2, 1.2, 3 },
                    { 8, 2, 1.5, 4 },
                    { 9, 3, 0.80000000000000004, 1 },
                    { 10, 3, 1.0, 2 },
                    { 11, 3, 1.2, 3 },
                    { 12, 3, 1.5, 4 },
                    { 13, 4, 0.80000000000000004, 1 },
                    { 14, 4, 1.0, 2 },
                    { 15, 4, 1.2, 3 },
                    { 16, 4, 1.5, 4 },
                    { 17, 5, 0.80000000000000004, 1 },
                    { 18, 5, 1.0, 2 },
                    { 19, 5, 1.2, 3 },
                    { 20, 5, 1.5, 4 },
                    { 21, 6, 0.80000000000000004, 1 },
                    { 22, 6, 1.0, 2 },
                    { 23, 6, 1.2, 3 },
                    { 24, 6, 1.5, 4 },
                    { 25, 12, 1.0, 2 },
                    { 26, 13, 1.0, 2 },
                    { 27, 14, 1.0, 2 },
                    { 28, 15, 1.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Phone", "RoleId" },
                values: new object[,]
                {
                    { 1, "manager@mochacafe.com", "Cafe Manager", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000001", 1 },
                    { 2, "ahmed.cashier@mochacafe.com", "Ahmed Cashier", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000002", 2 },
                    { 3, "mona.cashier@mochacafe.com", "Mona Cashier", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000003", 2 },
                    { 4, "samir.captain@mochacafe.com", "Captain Samir", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000004", 3 },
                    { 5, "rania.captain@mochacafe.com", "Captain Rania", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000005", 3 },
                    { 6, "tarek.barista@mochacafe.com", "Barista Tarek", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000006", 4 },
                    { 7, "nadia.barista@mochacafe.com", "Barista Nadia", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000007", 4 },
                    { 8, "karim.barista@mochacafe.com", "Barista Karim", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000008", 4 },
                    { 9, "ali.customer@email.com", "Regular Customer Ali", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000009", 5 },
                    { 10, "sara.customer@email.com", "Customer Sara", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000010", 5 },
                    { 11, "omar.customer@email.com", "Customer Omar", "AQAAAAIAAYagAAAAEHzl2JdIehlV5sHj9vJqIqFQ7y8R8Z5sT6pKq9r8x1Xa2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t", "+201000000011", 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CaptainId", "CashierId", "CustomerId", "OrderDate", "Status", "TableId", "Total", "WaiterId" },
                values: new object[,]
                {
                    { 1, 4, 2, 9, new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 1, 142.0, 6 },
                    { 2, 5, 3, 10, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Paid", 3, 198.0, 7 },
                    { 3, 4, 2, 11, new DateTime(2024, 1, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 5, 76.0, 8 },
                    { 4, 5, 3, 9, new DateTime(2024, 1, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 2, 124.0, 6 },
                    { 5, 4, 2, 10, new DateTime(2024, 1, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), "Cancelled", 4, 45.0, 7 }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "BillDate", "CashierId", "OrderId", "PaymentMethod" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Cash" },
                    { 2, new DateTime(2024, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Credit Card" },
                    { 3, new DateTime(2024, 1, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, "Cash" }
                });

            migrationBuilder.InsertData(
                table: "CancelledOrders",
                columns: new[] { "Id", "CancelledById", "CancelledDate", "OrderId", "Reason" },
                values: new object[] { 1, 2, new DateTime(2024, 1, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), 5, "Customer had to leave suddenly" });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "IsPayed", "ItemId", "OrderId", "Quantity", "SizeId" },
                values: new object[,]
                {
                    { 1, true, 1, 1, 2, 2 },
                    { 2, true, 12, 1, 1, 2 },
                    { 3, true, 9, 1, 1, 2 },
                    { 4, true, 4, 2, 1, 3 },
                    { 5, true, 6, 2, 1, 2 },
                    { 6, true, 13, 2, 2, 2 },
                    { 7, true, 5, 2, 1, 2 },
                    { 8, false, 3, 3, 1, 2 },
                    { 9, false, 15, 3, 1, 2 },
                    { 10, true, 2, 4, 1, 4 },
                    { 11, true, 14, 4, 1, 2 },
                    { 12, false, 16, 5, 1, 2 },
                    { 13, false, 12, 5, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CashierId",
                table: "Bills",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_OrderId",
                table: "Bills",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledOrders_CancelledById",
                table: "CancelledOrders",
                column: "CancelledById");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledOrders_OrderId",
                table: "CancelledOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSize_ItemId",
                table: "ItemSize",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSize_SizeId",
                table: "ItemSize",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SizeId",
                table: "OrderItems",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CaptainId",
                table: "Orders",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WaiterId",
                table: "Orders",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedOrders_ItemId",
                table: "ReturnedOrders",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnedOrders_OrderId",
                table: "ReturnedOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "CancelledOrders");

            migrationBuilder.DropTable(
                name: "ItemSize");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ReturnedOrders");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
