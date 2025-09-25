using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FreeSpinsGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "151d64a8-7378-4ee9-8916-996f2aa45d01", 0, "151d64a8-7378-4ee9-8919-776f2aa45d01", "testuser@abv.bg", false, true, false, null, "TESTUSER@ABV.BG", "TESTUSER@ABV.BG", "AQAAAAIAAYagAAAAEDMPkC/VR2nRUQCsB4l5IEOF43wMcTVz1hwoeqaPBS3ApbkGsLv9FpmPLLe3DoAMRw==", null, false, "151d64a8-9378-4ee9-8916-776f2aa45d01", false, "testuser@abv.bg" });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "IsActive", "MaxSpinsPerDay", "Name" },
                values: new object[,]
                {
                    { new Guid("651d64a8-7378-4ee9-8916-776f2aa45d01"), true, 5, "Test Campaign" },
                    { new Guid("651d64a8-7378-4ee9-8916-776f2aa45d02"), true, 5, "Second Test Campaign" },
                    { new Guid("651d64a8-7378-4ee9-8916-776f2aa45d03"), true, 5, "Third Test Campaign" }
                });

            migrationBuilder.InsertData(
                table: "PlayersCampaigns",
                columns: new[] { "CampaignId", "PlayerId" },
                values: new object[] { new Guid("651d64a8-7378-4ee9-8916-776f2aa45d01"), "151d64a8-7378-4ee9-8916-996f2aa45d01" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("651d64a8-7378-4ee9-8916-776f2aa45d02"));

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("651d64a8-7378-4ee9-8916-776f2aa45d03"));

            migrationBuilder.DeleteData(
                table: "PlayersCampaigns",
                keyColumns: new[] { "CampaignId", "PlayerId" },
                keyValues: new object[] { new Guid("651d64a8-7378-4ee9-8916-776f2aa45d01"), "151d64a8-7378-4ee9-8916-996f2aa45d01" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "151d64a8-7378-4ee9-8916-996f2aa45d01");

            migrationBuilder.DeleteData(
                table: "Campaigns",
                keyColumn: "CampaignId",
                keyValue: new Guid("651d64a8-7378-4ee9-8916-776f2aa45d01"));
        }
    }
}
