using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85eb5e0e-e0b0-49ff-aa89-9af21282822c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2fc8a96-16d5-4f6a-a04f-b6e27bf6c1c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0860240-f5bd-4114-97e6-80bb2fe98ec2");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BorrowEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "581ea05e-e8e0-4a1a-b7fa-dcc65d46fa35", null, "User", "USER" },
                    { "ca307351-3936-47cd-bfdc-b42d5de5cb59", null, "Admin", "ADMIN" },
                    { "faa70406-6577-4e0a-b5d3-5980301618f9", null, "Editor", "EDITOR" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Book", "BorrowDate", "BorrowEndDate", "Customer" },
                values: new object[] { 1, "Karagöz ve Hacivat", new DateTime(2024, 2, 7, 16, 40, 54, 438, DateTimeKind.Local).AddTicks(2705), new DateTime(2024, 2, 7, 16, 40, 54, 438, DateTimeKind.Local).AddTicks(2718), "Nihat Kahraman" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "581ea05e-e8e0-4a1a-b7fa-dcc65d46fa35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca307351-3936-47cd-bfdc-b42d5de5cb59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faa70406-6577-4e0a-b5d3-5980301618f9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85eb5e0e-e0b0-49ff-aa89-9af21282822c", null, "Editor", "EDITOR" },
                    { "b2fc8a96-16d5-4f6a-a04f-b6e27bf6c1c4", null, "User", "USER" },
                    { "e0860240-f5bd-4114-97e6-80bb2fe98ec2", null, "Admin", "ADMIN" }
                });
        }
    }
}
