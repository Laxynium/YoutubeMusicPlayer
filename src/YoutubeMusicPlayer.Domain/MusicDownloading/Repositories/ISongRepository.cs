using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Repositories
{
    public interface ISongRepository
    {
        Task<Song> GetAsync(SongId musicId);
        Task<IEnumerable<Song>> GetAllAsync();
        Task AddAsync(Song song);
        Task<bool> Exists(string youtubeId);
        Task RemoveAsync(SongId musicId);
    }
}