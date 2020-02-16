using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public class PlaylistId : ValueObject<PlaylistId>
    {
        public Guid Value { get; }

        public PlaylistId(Guid value)
        {
            if(value == default)
                throw new Exception("Playlist id cannot be empty.");

            Value = value;
        }

        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}