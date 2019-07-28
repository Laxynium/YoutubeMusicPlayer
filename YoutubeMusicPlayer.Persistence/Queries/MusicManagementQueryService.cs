using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using YoutubeMusicPlayer.Domain.MusicManagement.Queries;

namespace YoutubeMusicPlayer.Persistence.Queries
{
    public class MusicManagementQueryService : IMusicManagementQueryService
    {
        private readonly SQLiteAsyncConnection _connection;

        public MusicManagementQueryService(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<PlaylistDto>> GetAllPlaylists()
        {
            return await _connection.QueryAsync<PlaylistDto>(
                @"
SELECT 
    Id, 
    Name 
FROM Playlists"
            );
        }

        public async Task<IEnumerable<SongDto>> GetAllMusicNotOnPlaylist(Guid playlistId)
        {
            return await _connection.QueryAsync<SongDto>(@"
SELECT 
    So.Id AS SongId, 
    So.Title, 
    So.ImageSource 
FROM Songs AS So 
WHERE So.Id 
NOT IN (
    SELECT 
        PlSo.SongId 
    FROM PlaylistSongs AS PlSo 
    WHERE PlSo.PlaylistId = ?
)",
                playlistId);
        }

        public async Task<IEnumerable<SongDto>> GetAllSongsFromPlaylist(Guid playlistId)
        {
            return await _connection.QueryAsync<SongDto>(@"
SELECT
    So.Id AS SongId, 
    So.Title, 
    So.ImageSource 
FROM Songs As So
WHERE So.Id
IN (
    SELECT 
        PlSo.SongId 
    FROM PlaylistSongs AS PlSo 
    WHERE PlSo.PlaylistId = ?
)
",
                playlistId);
        }
    }
}