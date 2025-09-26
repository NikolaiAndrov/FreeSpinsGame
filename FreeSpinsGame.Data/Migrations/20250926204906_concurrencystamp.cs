using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeSpinsGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class concurrencystamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SpinsHistory",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SpinsHistory");
        }
    }
}
