using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;
using YoutubeMusicPlayer.MusicManagement.Domain.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects
{
    public class SongPath : ValueObject<SongPath>
    {
        public string Value { get; private set; }

        public static SongPath Create(string songTitle)
        {
            var path = songTitle.ToLowerInvariant().Replace(" ", "_").Replace(":","_");
            path += ".mp3";
            return new SongPath(path);
        }

        private SongPath() { }
        public SongPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new InvalidSongPathException();
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