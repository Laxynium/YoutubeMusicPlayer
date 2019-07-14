using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using YoutubeMusicPlayer.Domain.MusicPlaying;

namespace Tests.MusicPlaying.SongCoordinator
{
    [TestFixture]
    public class SongCoordinatorOnPreviousShould
    {
        [Test]
        public void WhenQueueNotEmptyAndCurrentlySelectedNotFirst_GoToPreviousElement()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song> { song1, song2 });
            coordinator.GoToNext();
            coordinator.GoToPrevious();
            coordinator.CurrentlySelected.Should().Be(song1);
        }

        [Test]
        public void WhenSongFromPlaylistWasSelectedAndThenGoneToSongInQueue_GoToThatSongInPlaylist()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());
            var song3 = new Song(Guid.NewGuid());
            var song4 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song> { song1, song2 });

            coordinator.Enqueue(song3);
            coordinator.Enqueue(song4);

            coordinator.GoToNext();
            coordinator.GoToPrevious();

            coordinator.CurrentlySelected.Should().Be(song1);
        }

        [Test]
        public void WhenSongFromPlaylistIsSelectedAndSomeMusicAddedToQueue_GoToPreviousSongFromPlaylist()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());
            var song3 = new Song(Guid.NewGuid());
            var song4 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song> { song1, song2 });

            coordinator.GoToNext();
            coordinator.Enqueue(song3);
            coordinator.Enqueue(song4);
            coordinator.GoToPrevious();

            coordinator.CurrentlySelected.Should().Be(song1);
        }

        [Test]
        public void WhenSongIsFirstFromPlaylist_GoToLastOneInPlaylist()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song> { song1, song2 });

            coordinator.GoToPrevious();

            coordinator.CurrentlySelected.Should().Be(song2);
        }
    }
}