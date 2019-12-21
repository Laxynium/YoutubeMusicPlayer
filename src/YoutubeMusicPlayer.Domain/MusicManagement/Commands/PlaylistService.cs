using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }
        public async Task Create(string name, IEnumerable<Guid> songIds = null)
        {
            var playlist = new Playlist(PlaylistName.FromString(name), songIds?.Select(x=>new Song(SongId.FromGuid(x))) ?? Enumerable.Empty<Song>());

            await _playlistRepository.SaveAsync(playlist);
        }

        public async Task UpdateName(Guid playlistId, string newName)
        {
            Playlist playlist = await _playlistRepository.GetAsync(playlistId)
                                ?? throw new Exception($"Invalid {nameof(playlistId)}");
            playlist.ChangeName(PlaylistName.FromString(newName));
            await _playlistRepository.SaveAsync(playlist);
        }

        public async Task AddSongsToExisting(Guid playlistId, IEnumerable<Guid> songIds)
        {
            Playlist playlist = await _playlistRepository.GetAsync(playlistId)
                                ?? throw new Exception($"Invalid {nameof(playlistId)}");
            playlist.AddSongs(songIds.Select(x => new Song(SongId.FromGuid(x))));
            await _playlistRepository.SaveAsync(playlist);
        }

        public async Task AddSongToExisting(Guid playlistId, Guid songId)
        {
            Playlist playlist = await _playlistRepository.GetAsync(playlistId)
                                ?? throw new Exception($"Invalid {nameof(playlistId)}");
            playlist.AddSong(new Song(SongId.FromGuid(songId)));
            await _playlistRepository.SaveAsync(playlist);
        }

        public async Task RemoveSongsFromExisting(Guid playlistId, IEnumerable<Guid> songIds)
        {
            Playlist playlist = await _playlistRepository.GetAsync(playlistId)
                                ?? throw new Exception($"Invalid {nameof(playlistId)}");

            playlist.RemoveSongs(songIds.Select(x => SongId.FromGuid(x)));

            await _playlistRepository.SaveAsync(playlist);
        }

        public async Task RemoveSongFromExisting(Guid playlistId, Guid songId)
        {
            Playlist playlist = await _playlistRepository.GetAsync(playlistId) 
                                ?? throw new Exception($"Invalid {nameof(playlistId)}");
            playlist.RemoveSong(SongId.FromGuid(songId));
            await _playlistRepository.SaveAsync(playlist);
        }

        public async Task Remove(Guid playlistId)
        {
            if(!await _playlistRepository.ExistsAsync(playlistId))
                throw new Exception($"Invalid {nameof(playlistId)}");

            await _playlistRepository.RemoveAsync(playlistId);
        }
    }
}