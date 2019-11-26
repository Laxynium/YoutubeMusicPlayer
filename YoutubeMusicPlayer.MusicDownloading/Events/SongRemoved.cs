using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.Events
{
    internal class SongRemoved : IEvent
    {
        public Guid SongId { get; }

        public SongRemoved(Guid songId)
        {
            SongId = songId;
        }
    }
}