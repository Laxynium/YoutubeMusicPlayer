using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public class SongPosition : ValueObject<SongId>
    {
        public int Value { get; private set; }

        public SongPosition(int value)
        {
            if(value < 0 )
                throw new Exception($"Position cannot be negative.");

            Value = value;
        }

        public static implicit operator int(SongPosition position)
        {
            return position.Value;
        }

        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }

        public bool InRange(in int songsCount)
        {
            return Value < songsCount;
        }
    }
}