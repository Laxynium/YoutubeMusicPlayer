using System;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Events
{
    public class SongRemoved : IEvent
    {
        public Guid SongId { get; }

        public SongRemoved(Guid songId)
        {
            SongId = songId;
        }
    }
}