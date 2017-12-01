using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Repositories
{
    public interface IDownloadServerRepository
    {
        Task InitializeAsync();

        Task<IEnumerable<DownloadServer>> GetAllAsync();

        Task<DownloadServer> GetAsync(int id);

        Task AddAsync(DownloadServer downloadServer);

    }
}
