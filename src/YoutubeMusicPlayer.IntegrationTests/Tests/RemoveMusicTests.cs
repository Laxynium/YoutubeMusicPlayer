using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YoutubeMusicPlayer.MusicManagement.Application.Commands;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.EF;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class RemoveMusicTests : IntegrationTest
    {
        [Fact]
        public async Task Song_is_removed_from_filesystem_and_database()
        {
            var downloadSong = new AddSongFromYoutube(Guid.NewGuid(), "dqVZaN4lnwQ", "Never Go Away", string.Empty,"source.com/img");
            var removeSong = new RemoveSong(downloadSong.SongId);

            await CommandDispatcher.DispatchAsync(downloadSong);

            string songPath;
            using (var context = new MusicManagementDbContext(ContextOptions))
            {
                songPath = (await EntityFrameworkQueryableExtensions.FirstAsync(context.MainPlaylist)).Songs.First().SongPath;
            }
            
            await CommandDispatcher.DispatchAsync(removeSong);


            using var musicManagementContext = new MusicManagementDbContext(ContextOptions);

            var mainPlaylist = await EntityFrameworkQueryableExtensions.SingleAsync(musicManagementContext.MainPlaylist);

            mainPlaylist.Songs.Should().BeEmpty();

            File.Exists(Path.Combine(MusicDirectory, songPath)).Should().BeFalse();
        }
    }
}