using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public class PlaylistName : ValueObject<PlaylistName>
    {
        public string Value { get; }

        public static PlaylistName FromString(string value)
            => new PlaylistName(value);

        private PlaylistName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(value)} cannot be empty.");

            Value = value;
        }

        public static implicit operator string(PlaylistName name)
            => name.Value;

        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}