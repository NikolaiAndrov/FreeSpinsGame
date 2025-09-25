using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FreeSpinsGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDbIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "651d64a8-7378-4ee9-8916-776f2bb45d01", null, "Admin", "ADMIN" },
                    { "651d64a8-7378-4ee9-8916-776f2nm45d01", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "651d64a8-7378-4ee9-8916-776f2bb45d01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "651d64a8-7378-4ee9-8916-776f2nm45d01");
        }
    }
}
