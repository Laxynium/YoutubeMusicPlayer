using System;

namespace YoutubeMusicPlayer.Domain.MusicPlaying
{
    public class Song
    {
        public Guid Id { get; }

        public Song(Guid id)
        {
            Id = id;
        }
    }
}