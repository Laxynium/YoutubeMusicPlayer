using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore.Migrations.Module1
{
    public partial class SetupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module1_entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module1_entities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "module1_entities");
        }
    }
}
