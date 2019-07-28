using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.SharedKernel
{
    public class SongId : ValueObject<SongId>
    {
        public Guid Value { get; }

        protected SongId(Guid value)
        {
            if(value == default)
                throw new Exception($"{nameof(value)} cannot have default value.");
            Value = value;
        }
        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
        public static SongId FromGuid(Guid guid)
            => new SongId(guid);

        public static implicit operator Guid(SongId self) => self.Value;

        public override string ToString() => Value.ToString();
    }
}