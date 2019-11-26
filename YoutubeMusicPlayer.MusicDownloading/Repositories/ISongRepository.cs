using System.Threading.Tasks;
using YoutubeMusicPlayer.MusicDownloading.Domain;

namespace YoutubeMusicPlayer.MusicDownloading.Repositories
{
    public interface ISongRepository
    {
        Task AddAsync(Song song);
        Task<bool> Exists(string youtubeId);
    }
}