using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public static class SongListExtension
    {
        public static IEnumerable<Song> UpdatePosition(this IEnumerable<Song> songs)
        {
            var songsList = songs.ToList();
            return songsList.Zip(Enumerable.Range(0, songsList.Count),(x,i)=>{x.SetPosition(i);
                    return x;
                }).ToList();
        }
    }
    public class Playlist
    {
        public Guid Id { get; }
        public PlaylistName Name { get; private set; }

        private readonly IList<Song> _songs;
        public IReadOnlyList<Song> Songs => _songs.ToList().AsReadOnly();

        public Playlist(PlaylistName name, IEnumerable<Song> songs = null):this(Guid.NewGuid(), name,songs)
        {
        }

        public Playlist(Guid id, PlaylistName name, IEnumerable<Song> songs = null)
        {
            
            Id = id;
            Name = name;
            _songs = songs?.UpdatePosition()?.ToList() ?? new List<Song>();
        }

        public void ChangeName(PlaylistName newName)
        {
            Name = newName;
        }

        public void AddSongs(IEnumerable<Song> songs)
        {
            songs.ToList().ForEach(
                x =>
                {
                    x.SetPosition(_songs.Count);
                    _songs.Add(x);
                });
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
            songsToRemove.ForEach(s=>_songs.Remove(s));
        }
    }
}