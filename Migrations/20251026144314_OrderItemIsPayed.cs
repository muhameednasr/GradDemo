using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradDemo.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemIsPayed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
       
            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
