﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ClueBot.Migrations
{
    public partial class Migration : Microsoft.EntityFrameworkCore.Migrations.Migration 
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExperiencePoints",
                columns: table => new
                {
                    UserId = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperiencePoints", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperiencePoints");
        }
    }
}
