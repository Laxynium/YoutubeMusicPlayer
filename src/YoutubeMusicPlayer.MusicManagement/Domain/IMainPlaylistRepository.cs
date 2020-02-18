using System.Threading.Tasks;
using YoutubeMusicPlayer.MusicManagement.Domain.Entities;

namespace YoutubeMusicPlayer.MusicManagement.Domain
{
    public interface IMainPlaylistRepository
    {
        Task<MainPlaylist> GetAsync();
        Task UpdateAsync(MainPlaylist mainPlaylist);
    }
}