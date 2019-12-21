using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicDownloading.Application.Repositories;
using YoutubeMusicPlayer.MusicDownloading.Domain;

namespace YoutubeMusicPlayer.MusicDownloading.Infrastructure
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicDownloadingDbContext _dbContext;

        public SongRepository(MusicDownloadingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Song> GetAsync(Guid songId)
        {
            return await _dbContext.Songs.SingleOrDefaultAsync(x => x.Id == songId);
        }

        public async Task<bool> Exists(string youtubeId)
        {
            return !(await _dbContext.Songs.FirstOrDefaultAsync(x => x.YtId == youtubeId) is null);
        }

        public async Task AddAsync(Song song)
        {
            await _dbContext.Songs.AddAsync(song);
        }

        public Task RemoveAsync(Song song)
        {
            _dbContext.Songs.Remove(song);
            return Task.CompletedTask;
        }
    }
}