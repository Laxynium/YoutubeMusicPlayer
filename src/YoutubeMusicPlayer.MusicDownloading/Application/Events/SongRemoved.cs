using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Events
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