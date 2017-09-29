using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Repositories
{
    public interface IMusicRepository
    {
        Task Initialize();

        Task<Music> GetAsync(string id);

        Task<IEnumerable<Music>> GetAllAsync();

        Task AddAsync(Music music);

        Task UpdateAsync(Music music);
    }
}
