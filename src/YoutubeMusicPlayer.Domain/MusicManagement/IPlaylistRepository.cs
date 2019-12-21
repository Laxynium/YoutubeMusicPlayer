using System;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public interface IPlaylistRepository
    {
        Task<AllSongsPlaylist> GetAsync();
        Task<Playlist> GetAsync(Guid playlistId);
        Task<bool> ExistsAsync(Guid playlistId);
        Task SaveAsync(PlaylistBase playlist);
        Task RemoveAsync(Guid playlistId);
    }
}