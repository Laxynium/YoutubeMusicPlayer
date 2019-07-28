using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicManagement.Queries
{
    public interface IMusicManagementQueryService
    {
        Task<IEnumerable<PlaylistDto>> GetAllPlaylists();
        Task<IEnumerable<SongDto>> GetAllMusicNotOnPlaylist(Guid playlistId);
        Task<IEnumerable<SongDto>> GetAllSongsFromPlaylist(Guid playlistId);
    }
}