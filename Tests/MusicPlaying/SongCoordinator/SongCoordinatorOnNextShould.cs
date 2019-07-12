using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using YoutubeMusicPlayer.Domain.MusicPlaying;

namespace Tests.MusicPlaying.SongCoordinator
{
    [TestFixture]
    public class SongCoordinatorOnNextShould
    {
        [Test]
        public void WhenQueueNotEmptyGoToNextSongInQueue()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());
            var song3 = new Song(Guid.NewGuid());
            var song4 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song>{song1,song2});
            coordinator.Enqueue(song3);
            coordinator.Enqueue(song4);

            coordinator.GoToNext();

            coordinator.CurrentlySelected.Should().Be(song3);
        }

        [Test]
        public void WhenQueueEmptyGoToNextSongInPlaylist()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song> { song1, song2 });

            coordinator.GoToNext();

            coordinator.CurrentlySelected.Should().Be(song2);
        }

        [Test]
        public void WhenQueueEmptyAndCurrentSelectIsLastGoToFirstElement()
        {
            var song1 = new Song(Guid.NewGuid());
            var song2 = new Song(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<Song> { song1, song2 });

            coordinator.GoToNext(); //go to song 2
            coordinator.GoToNext();

            coordinator.CurrentlySelected.Should().Be(song1);
        }
    }
}
