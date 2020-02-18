using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "music_management$main_playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_music_management$main_playlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "music_management$main_playlist_song",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MainPlaylistId = table.Column<Guid>(nullable: false),
                    YtId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    SongPath = table.Column<string>(nullable: true),
                    ThumbnailUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_music_management$main_playlist_song", x => new { x.MainPlaylistId, x.Id });
                    table.ForeignKey(
                        name: "FK_music_management$main_playlist_song_music_management$main_playlist_MainPlaylistId",
                        column: x => x.MainPlaylistId,
                        principalTable: "music_management$main_playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "music_management$main_playlist",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c90db7f6-ad0f-45da-879f-6ba326401f17"), "Main Playlist" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "music_management$main_playlist_song");

            migrationBuilder.DropTable(
                name: "music_management$main_playlist");
        }
    }
}
