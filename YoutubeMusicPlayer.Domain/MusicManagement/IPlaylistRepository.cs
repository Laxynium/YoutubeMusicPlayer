using System;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public interface IPlaylistRepository
    {
        Task RemoveAsync(Guid playlistId);
        Task<Playlist> GetAsync(Guid playlistId);
        Task SaveAsync(Playlist playlist);
        Task<bool> ExistsAsync(Guid playlistId);
    }
}