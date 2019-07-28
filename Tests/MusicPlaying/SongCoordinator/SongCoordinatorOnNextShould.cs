using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using YoutubeMusicPlayer.Domain.MusicPlaying;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace Tests.MusicPlaying.SongCoordinator
{
    [TestFixture]
    public class SongCoordinatorOnNextShould
    {
        [Test]
        public void WhenQueueNotEmptyGoToNextSongInQueue()
        {
            var song1 = SongId.FromGuid(Guid.NewGuid());
            var song2 = SongId.FromGuid(Guid.NewGuid());
            var song3 = SongId.FromGuid(Guid.NewGuid());
            var song4 = SongId.FromGuid(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<SongId>{song1,song2});
            coordinator.Enqueue(song3);
            coordinator.Enqueue(song4);

            coordinator.GoToNext();

            coordinator.CurrentlySelected.Should().Be(song3);
        }

        [Test]
        public void WhenQueueEmptyGoToNextSongInPlaylist()
        {
            var song1 = SongId.FromGuid(Guid.NewGuid());
            var song2 = SongId.FromGuid(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<SongId> { song1, song2 });

            coordinator.GoToNext();

            coordinator.CurrentlySelected.Should().Be(song2);
        }

        [Test]
        public void WhenQueueEmptyAndCurrentSelectIsLastGoToFirstElement()
        {
            var song1 = SongId.FromGuid(Guid.NewGuid());
            var song2 = SongId.FromGuid(Guid.NewGuid());

            var coordinator = new YoutubeMusicPlayer.Domain.MusicPlaying.SongCoordinator(new List<SongId> { song1, song2 });

            coordinator.GoToNext(); //go to song 2
            coordinator.GoToNext();

            coordinator.CurrentlySelected.Should().Be(song1);
        }
    }
}
