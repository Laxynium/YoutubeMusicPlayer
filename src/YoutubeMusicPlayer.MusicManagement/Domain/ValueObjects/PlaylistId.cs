using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;

namespace YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects
{
    public class PlaylistId : ValueObject<PlaylistId>
    {
        public Guid Value { get; private set; }

        public PlaylistId(Guid value)
        {
            if (value == default)
                throw new ArgumentException($"Song id cannot be empty.");
            Value = value;
        }
        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}