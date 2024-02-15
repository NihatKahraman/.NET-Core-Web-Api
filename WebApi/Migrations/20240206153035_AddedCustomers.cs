using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26945fcd-5a7e-4502-83fa-af2b7fc67f50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3131996f-f6fa-4a0c-b9b8-c2935f3ba808");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "768448cc-70eb-40ad-932a-091ef56647c8");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<double>(type: "float", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85eb5e0e-e0b0-49ff-aa89-9af21282822c", null, "Editor", "EDITOR" },
                    { "b2fc8a96-16d5-4f6a-a04f-b6e27bf6c1c4", null, "User", "USER" },
                    { "e0860240-f5bd-4114-97e6-80bb2fe98ec2", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Age", "Gender", "Name", "PhoneNumber" },
                values: new object[] { 1, 26, "Male", "Nihat Kahraman", 5551234567.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26945fcd-5a7e-4502-83fa-af2b7fc67f50", null, "User", "USER" },
                    { "3131996f-f6fa-4a0c-b9b8-c2935f3ba808", null, "Admin", "ADMIN" },
                    { "768448cc-70eb-40ad-932a-091ef56647c8", null, "Editor", "EDITOR" }
                });
        }
    }
}
