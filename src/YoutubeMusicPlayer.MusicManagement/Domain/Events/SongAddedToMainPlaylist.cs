using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Domain.Entities;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Events
{
    public class SongAddedToMainPlaylist : IEvent
    {
        public Song Song { get; }

        public SongAddedToMainPlaylist(Song song)
        {
            Song = song;
        }
    }
}