using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public class SongId : ValueObject<SongId>
    {
        public Guid Value { get; private set; }

        public SongId():this(Guid.NewGuid())
        {
        }
        public SongId(Guid value)
        {
            if(value == default)
                throw new Exception($"Song id cannot be empty.");
            Value = value;
        }
        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}