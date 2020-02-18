using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeMusicPlayer.IntegrationTests.Infrastructure.EFCore.Migrations.Module2
{
    public partial class SetupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module2_entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module2_entities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "module2_entities");
        }
    }
}
