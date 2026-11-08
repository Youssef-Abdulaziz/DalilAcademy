using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DalilAcademy.Infra.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "FullName" },
                values: new object[,]
                {
                    { 1, "Ahmad Saleh" },
                    { 2, "Lina Haddad" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "InstructorId", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, 50m, "C# Fundamentals" },
                    { 2, 1, 80m, "ASP.NET Core Web API" },
                    { 3, 2, 60m, "PostgreSQL Essentials" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
