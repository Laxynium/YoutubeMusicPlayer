using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Repositories
{
    public interface ISongRepository
    {
        Task AddAsync(Song song);
    }
}