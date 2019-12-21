using System;
using System.IO;
using System.Threading.Tasks;
using ChinhDo.Transactions.FileManager;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YoutubeMusicPlayer.MusicDownloading.Application.Commands;
using YoutubeMusicPlayer.MusicDownloading.Domain;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests.MusicDownloading
{
    public class DownloadMusicIntegrationTests : IntegrationTest
    {
        [Fact]
        public async Task Operation_is_successful_when_no_problems_with_download()
        {
            var command = new DownloadSongCommand(Guid.NewGuid(), "dqVZaN4lnwQ", "Never Go Away", "source.com/img");

            await Dispatcher.DispatchAsync(command);

            using var dbContext = new MusicDownloadingDbContext(
                new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options);
            var createdSong = await dbContext.Songs.SingleAsync();

            AssertThatIsCorrectlySavedToDatabase(createdSong);
            AssertThatIsCorrectlySavedToFileSystem(createdSong);
        }

        private static void AssertThatIsCorrectlySavedToDatabase(Song createdSong)
        {
            createdSong.YtId.Should().Be("dqVZaN4lnwQ");
            createdSong.ImageSource.Should().Be("source.com/img");
            createdSong.Title.Should().NotBeNullOrWhiteSpace();
            createdSong.FilePath.Should().NotBeNullOrWhiteSpace();
        }

        private static void AssertThatIsCorrectlySavedToFileSystem(Song createdSong)
        {
            File.Exists(createdSong.FilePath).Should().BeTrue();
        }
    }
}
