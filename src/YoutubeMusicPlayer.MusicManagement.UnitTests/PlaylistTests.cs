using System;
using FluentAssertions;
using Xunit;
using YoutubeMusicPlayer.MusicManagement.Domain;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;

namespace YoutubeMusicPlayer.MusicManagement.UnitTests
{
    public class PlaylistTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void Playlist_name_cannot_be_empty(string name)
        {
            Func<PlaylistName> createPlaylistName = () => new PlaylistName(name);
            createPlaylistName.Should().Throw<Exception>();
        }

        [Fact]
        public void Create_playlist_when_name_is_valid()
        {
            var playlistName = new PlaylistName("valid name");
            playlistName.Value.Should().Be("valid name");
        }
    }
}
