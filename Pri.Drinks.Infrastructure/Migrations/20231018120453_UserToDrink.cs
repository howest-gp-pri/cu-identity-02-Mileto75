using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pri.Drinks.Infrastructure.Migrations
{
    public partial class UserToDrink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Drinks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3805));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3807));

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3811));

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3812));

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3812));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3808));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 4, 53, 119, DateTimeKind.Utc).AddTicks(3809));

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_ApplicationUserId",
                table: "Drinks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_AspNetUsers_ApplicationUserId",
                table: "Drinks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_AspNetUsers_ApplicationUserId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_ApplicationUserId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Drinks");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1952));

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1961));

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1964));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1954));

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 10, 18, 12, 2, 31, 941, DateTimeKind.Utc).AddTicks(1955));
        }
    }
}
