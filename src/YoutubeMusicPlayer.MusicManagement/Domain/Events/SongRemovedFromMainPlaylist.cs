using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Events
{
    public class SongRemovedFromMainPlaylist : IEvent
    {
        public Guid SongId { get; }

        public SongRemovedFromMainPlaylist(Guid songId)
        {
            SongId = songId;
        }
    }
}