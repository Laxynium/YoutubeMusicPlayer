using System;
using FluentAssertions;
using Xunit;
using YoutubeMusicPlayer.MusicManagement.Domain;

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

        [Fact]
        public void Song_is_added_at_the_end_of_list()
        {
            var playlist = Playlist.CreateNormal(new PlaylistName("valid_name"));
            var song1 = new SongId();
            var song2 = new SongId();
            var song3 = new SongId();

            playlist.AddSong(song1);
            playlist.AddSong(song2);
            playlist.AddSong(song3);

            playlist.Songs[0].Should().Be(song1);
            playlist.Songs[1].Should().Be(song2);
            playlist.Songs[2].Should().Be(song3);
        }

        [Fact]
        public void Moving_song_to_some_later_position()
        {
            var playlist = Playlist.CreateNormal(new PlaylistName("valid_name"));
            var song1 = new SongId();
            var song2 = new SongId();
            var song3 = new SongId();
            var song4 = new SongId();

            playlist.AddSong(song1);
            playlist.AddSong(song2);
            playlist.AddSong(song3);
            playlist.AddSong(song4);

            playlist.ChangePosition(new SongPosition(1), new SongPosition(3));

            playlist.Songs[0].Should().Be(song1);
            playlist.Songs[1].Should().Be(song3);
            playlist.Songs[2].Should().Be(song4);
            playlist.Songs[3].Should().Be(song2);
        }

        [Fact]
        public void Moving_song_to_some_earlier_position()
        {
            var playlist = Playlist.CreateNormal(new PlaylistName("valid_name"));
            var song1 = new SongId();
            var song2 = new SongId();
            var song3 = new SongId();
            var song4 = new SongId();

            playlist.AddSong(song1);
            playlist.AddSong(song2);
            playlist.AddSong(song3);
            playlist.AddSong(song4);

            playlist.ChangePosition(new SongPosition(3), new SongPosition(1));

            playlist.Songs[0].Should().Be(song1);
            playlist.Songs[1].Should().Be(song4);
            playlist.Songs[2].Should().Be(song2);
            playlist.Songs[3].Should().Be(song3);
        }

    }
}
