using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public class Playlist : PlaylistBase
    {
        public Playlist(PlaylistName name, IEnumerable<Song> songs = null):this(PlaylistId.FromGuid(new Guid()), name,songs)
        {
        }

        public Playlist(PlaylistId id, PlaylistName name, IEnumerable<Song> songs = null) : base(id, name, songs)
        {
        }

        public void ChangeName(PlaylistName newName)
        {
            Name = newName;
        }
    }
}