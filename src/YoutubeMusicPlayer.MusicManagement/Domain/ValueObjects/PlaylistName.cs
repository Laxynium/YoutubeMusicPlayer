using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.BuildingBlocks;
using YoutubeMusicPlayer.MusicManagement.Domain.Exceptions;

namespace YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects
{
    public class PlaylistName : ValueObject<PlaylistName>
    {
        public string Value { get; private set; }

        private PlaylistName() { }
        public PlaylistName(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
                throw new InvalidPlaylistNameException();
            Value = value;
        }
        protected override IEnumerable<object> GetProperties()
        {
            yield return Value;
        }
    }
}