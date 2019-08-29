using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.SharedKernel
{
    public class PlaylistId : ValueObject<PlaylistId>
    {
        public Guid Value { get; }

        protected PlaylistId(Guid value)
        {
            if(value == default)
                throw new Exception($"{nameof(value)} cannot have default value.");
            Value = value;
        }
        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
        public static PlaylistId FromGuid(Guid guid)
            => new PlaylistId(guid);

        public static implicit operator Guid(PlaylistId self) => self.Value;

        public override string ToString() => Value.ToString();
    }
}