using System;
using System.Threading.Tasks;
using ChinhDo.Transactions.FileManager;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YoutubeMusicPlayer.MusicDownloading.Application.Commands;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.Tests.MusicDownloading
{
    public class RemoveMusicIntegrationTests : IntegrationTest
    {
        [Fact]
        public async Task Song_is_removed_from_filesystem_and_database()
        {
            var songId = Guid.NewGuid();
            var command = new DownloadSongCommand(songId, "dqVZaN4lnwQ", "Never Go Away", "source.com/img");
            await Dispatcher.DispatchAsync(command);
            string songPath = "";
            using (var dbContext = new MusicDownloadingDbContext(
                new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options))
            {
                var createdSong = await dbContext.Songs.SingleAsync();
                songPath = createdSong.FilePath;
            }

            var removeCommand = new RemoveSongCommand(songId);
            await Dispatcher.DispatchAsync(removeCommand);

            using (var dbContext = new MusicDownloadingDbContext(
                new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options))
            {
                var songs = await dbContext.Songs.ToListAsync();
                songs.Should().BeEmpty();
            }
            var fileManager = new TxFileManager();
            fileManager.FileExists(songPath).Should().BeFalse();

        }
    }
}