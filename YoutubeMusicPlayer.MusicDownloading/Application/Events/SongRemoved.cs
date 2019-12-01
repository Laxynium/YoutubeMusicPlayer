using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Events
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