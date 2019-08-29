using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Domain.SharedKernel;
using YoutubeMusicPlayer.Persistence.DatabaseModels;

namespace YoutubeMusicPlayer.Persistence.Repositories
{
    internal class PlaylistRepository : IPlaylistRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        private static readonly string AllSongsPlaylistType = typeof(AllSongsPlaylist).ToString();
        public PlaylistRepository(SQLiteAsyncConnection connection)
        {
            this._connection = connection;
        }

        public async Task RemoveAsync(Guid playlistId)
        {
            await _connection.DeleteAsync(await _connection.GetWithChildrenAsync<PlaylistDb>(playlistId), true);
        }

        public async Task<AllSongsPlaylist> GetAsync() 
            => TooAllSongsPlaylist((await _connection.GetAllWithChildrenAsync<PlaylistDb>(x => x.Type == AllSongsPlaylistType)).Single());

        public async Task<bool> ExistsAsync(Guid playlistId) 
            => await _connection.FindAsync<PlaylistDb>(playlistId) != null;

        public async Task<Playlist> GetAsync(Guid playlistId) 
            => ToPlaylist(await _connection.FindWithChildrenAsync<PlaylistDb>(playlistId));

        public async Task SaveAsync(PlaylistBase playlist)
        {
            var dbPlaylist = FromPlaylist(playlist);
            await _connection.RunInTransactionAsync(c =>
            {
                var playlistDb = c.FindWithChildren<PlaylistDb>(playlist.Id.Value, true);
                if(playlistDb != null)
                    c.Delete(playlistDb, true);
                c.InsertOrReplaceWithChildren(dbPlaylist, true);
                c.UpdateWithChildren(dbPlaylist);
            });
        }

        private static PlaylistDb FromPlaylist(PlaylistBase playlist) => 
            new PlaylistDb
            {
                Id = playlist.Id
                ,Name = playlist.Name,
                Songs = playlist.Songs.Select(x=>FromSong(playlist.Id,x)).ToList(),
                Type = typeof(AllSongsPlaylist).ToString()

            };

        private static PlaylistSongDb FromSong(Guid playlistId, Song x)
            => new PlaylistSongDb
            {
                Id = $"{playlistId}_{x.Id}",
                SongId = x.Id,
                Position = x.Position,
                PlaylistId = playlistId,
            };

        private static Playlist ToPlaylist(PlaylistDb p) => 
            new Playlist(PlaylistId.FromGuid(p.Id),PlaylistName.FromString(p.Name),
                p.Songs?.Select(x=>new Song(SongId.FromGuid(x.SongId), x.Position)) ?? new List<Song>());

        private static AllSongsPlaylist TooAllSongsPlaylist(PlaylistDb p) =>
            new AllSongsPlaylist(PlaylistId.FromGuid(p.Id), PlaylistName.FromString(p.Name),
                p.Songs?.Select(x => new Song(SongId.FromGuid(x.SongId), x.Position)) ?? new List<Song>());
    }
}