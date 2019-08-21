using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.SharedKernel
{
    public class SongPath : ValueObject<SongPath>
    {
        public string Value { get; }

        public SongPath(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"Path cannot be empty.");
            Value = path;
        }

        public static implicit operator string(SongPath path)
        {
            return path.Value;
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}