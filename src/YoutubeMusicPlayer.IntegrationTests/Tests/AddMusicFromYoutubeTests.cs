using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YoutubeMusicPlayer.MusicManagement.Application.Commands;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.EF;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class AddMusicFromYoutubeTests : IntegrationTest
    {
        [Fact]
        public async Task Operation_is_successful_when_no_problems_with_download()
        {
            var command = new AddSongFromYoutube(Guid.NewGuid(), "dqVZaN4lnwQ","Never Go Away", string.Empty,"source.com/img");

            await CommandDispatcher.DispatchAsync(command);

            using var musicManagementContext = new MusicManagementDbContext(ContextOptions);

            var mainPlaylist = await musicManagementContext.MainPlaylist.SingleAsync();

            mainPlaylist.Songs.Should().HaveCount(1);
            var mainPlaylistSong = System.Linq.Enumerable.First(mainPlaylist.Songs);

            mainPlaylistSong.Should().BeEquivalentTo(
                new MusicManagement.Domain.Entities.Song(
                    new SongId(command.SongId),
                    command.YtId,
                    command.Title,
                    string.Empty,
                    SongPath.Create(command.Title),
                    command.ThumbnailUrl
                )
            );

            File.Exists(Path.Combine(MusicDirectory, mainPlaylistSong.SongPath)).Should().BeTrue();
        }
    }
}
