using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeMusicPlayer.Framework.BuildingBlocks;
using YoutubeMusicPlayer.MusicManagement.Domain.Events;
using YoutubeMusicPlayer.MusicManagement.Domain.Exceptions;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Entities
{
    public class MainPlaylist : Entity<PlaylistId>
    {
        public PlaylistName Name { get; private set; }

        private List<Song> _songs = new List<Song>();
        public IEnumerable<Song> Songs { get => _songs; private set => _songs = value.ToList(); }
        private MainPlaylist():base(new PlaylistId(Guid.NewGuid())) { }
        public MainPlaylist(PlaylistId id, PlaylistName name) : base(id)
        {
            Name = name;
        }

        public void AddSong(Song song)
        {
            if(_songs.Any(x=>x.Id == song.Id))
                throw new SongAlreadyOnMainPlaylistException(song.Title);

            _songs.Add(song);

            Apply(new SongAddedToMainPlaylist(song));
        }

        public Song RemoveSong(SongId songId)
        {
            var song = _songs.FirstOrDefault(x => x.Id == songId);
            if (song is null) return null;
            _songs.Remove(song);
            Apply(new SongRemovedFromMainPlaylist(song.Id.Value));
            return song;

        }
    }
}