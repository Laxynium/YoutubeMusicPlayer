using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public class PlaylistBase : Entity<PlaylistId> 
    {
        private readonly IList<Song> _songs;

        public PlaylistName Name { get; protected set; }

        public IReadOnlyList<Song> Songs => _songs.ToList().AsReadOnly();

        public PlaylistBase(PlaylistId id, PlaylistName name,IEnumerable<Song> songs = null) : base(id)
        {
            Name = name;
            _songs = songs?.ToList() ?? new List<Song>();
        }

        public void AddSongs(IEnumerable<Song> songs)
        {
            foreach (var x in songs.ToList())
            {
                x.SetPosition(_songs.Count);
                _songs.Add(x);
            }
        }

        public void AddSong(Song song)
        {
            Guard.Against.Null(song,nameof(song));

            song.SetPosition(_songs.Count);
            _songs.Add(song);
        }

        public void RemoveSong(SongId id)
        {
            var song = _songs.SingleOrDefault(x => x.Id == id);
            if (song is null)
                return;
            _songs.Remove(song);
        }

        public void RemoveSongs(IEnumerable<SongId> songIds)
        {
            var songsToRemove = _songs.Where(s => songIds.Contains(s.Id)).ToList();
            foreach (var s in songsToRemove) _songs.Remove(s);
        }
    }
}