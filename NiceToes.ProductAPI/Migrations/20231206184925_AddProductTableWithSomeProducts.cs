using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NiceToes.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTableWithSomeProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Winter", "For cold walks.", null, "https://localhost:7000/Images/1.jpg", "Boots", 400.0 },
                    { 2, "Winter", "For business meetings.", null, "https://localhost:7000/Images/2.jpg", "Shoes", 450.0 },
                    { 3, "Summer", "For doing sports.", null, "https://localhost:7000/Images/3.jpg", "Trainers", 300.0 },
                    { 4, "Scene", "For dancing.", null, "https://localhost:7000/Images/4.jpg", "Ballet shoes", 100.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
