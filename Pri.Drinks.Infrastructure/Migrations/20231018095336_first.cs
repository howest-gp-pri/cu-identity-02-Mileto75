using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.Drinks.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AlcoholPercentage = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrinkProperty",
                columns: table => new
                {
                    DrinksId = table.Column<int>(type: "int", nullable: false),
                    PropertiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkProperty", x => new { x.DrinksId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_DrinkProperty_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkProperty_Properties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Created", "Deleted", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(7994), null, "Beer", null },
                    { 2, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(8001), null, "Spirits", null }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Created", "Deleted", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(8003), null, "Sweet", null },
                    { 2, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(8005), null, "bitter", null }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "AlcoholPercentage", "CategoryId", "Created", "Deleted", "Image", "Name", "Updated" },
                values: new object[] { 1, 8, 1, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(8010), null, null, "Duvel", null });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "AlcoholPercentage", "CategoryId", "Created", "Deleted", "Image", "Name", "Updated" },
                values: new object[] { 2, 38, 2, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(8011), null, null, "Tequila", null });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "AlcoholPercentage", "CategoryId", "Created", "Deleted", "Image", "Name", "Updated" },
                values: new object[] { 3, 35, 2, new DateTime(2023, 10, 18, 9, 53, 36, 516, DateTimeKind.Utc).AddTicks(8014), null, null, "Irish", null });

            migrationBuilder.InsertData(
                table: "DrinkProperty",
                columns: new[] { "DrinksId", "PropertiesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkProperty_PropertiesId",
                table: "DrinkProperty",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_CategoryId",
                table: "Drinks",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkProperty");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
