using System;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public class Song : Entity<SongId>
    {
        public int Position { get; private set; }

        public Song(SongId id) : base(id)
        {
        }

        public Song(SongId id, int position):this(id)
        {
            Position = position;
        }

        public void SetPosition(int position)
        {
            Position = position;
        }
    }
}