using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Domain.SharedKernel;
using YoutubeMusicPlayer.Persistence.DatabaseModels;

namespace YoutubeMusicPlayer.Persistence.Repositories
{
    internal class PlaylistRepository : IPlaylistRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public PlaylistRepository(SQLiteAsyncConnection connection)
        {
            this._connection = connection;
        }

        public async Task RemoveAsync(Guid playlistId)
        {
            await _connection.DeleteAsync(await _connection.GetWithChildrenAsync<PlaylistDb>(playlistId), true);
        }

        public async Task<bool> ExistsAsync(Guid playlistId)
        {
            return await _connection.FindAsync<PlaylistDb>(playlistId) != null;
        }

        public async Task<Playlist> GetAsync(Guid playlistId)
        {
            return ToPlaylist(await _connection.FindWithChildrenAsync<PlaylistDb>(playlistId));
        }

        public async Task SaveAsync(Playlist playlist)
        {
            if (await _connection.FindAsync<PlaylistDb>(playlist.Id) == null)
                await _connection.InsertOrReplaceWithChildrenAsync(FromPlaylist(playlist),true);
            else
                await _connection.InsertOrReplaceWithChildrenAsync(FromPlaylist(playlist),true);
        }

        private static PlaylistDb FromPlaylist(Playlist playlist) => 
            new PlaylistDb{Id = playlist.Id,Name = playlist.Name,Songs = playlist.Songs.Select(x=>
                new PlaylistSongDb
                {
                    Id = $"{playlist.Id}_{x.Id}",
                    SongId = x.Id,
                    Position = x.Position,
                    PlaylistId = playlist.Id
                })
                .ToList()};

        private static Playlist ToPlaylist(PlaylistDb p) => 
            new Playlist(p.Id,PlaylistName.FromString(p.Name),p.Songs?.Select(x=>new Song(SongId.FromGuid(x.SongId), x.Position)) ?? new List<Song>());
    }
}