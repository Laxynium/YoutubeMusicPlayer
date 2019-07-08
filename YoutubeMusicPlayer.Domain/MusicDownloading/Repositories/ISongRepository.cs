using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Repositories
{
    public interface ISongRepository
    {
        Task<Song> GetAsync(Guid musicId);
        Task<IEnumerable<Song>> GetAllAsync();
        Task AddAsync(Song song);
        Task<bool> Exists(string youtubeId);
        Task RemoveAsync(Guid musicId);
    }
}