using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public class PlaylistName : ValueObject<PlaylistName>
    {
        public string Value { get; private set; }

        private PlaylistName() { }
        public PlaylistName(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new Exception($"Playlist name cannot by empty.");
            Value = value;
        }
        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}