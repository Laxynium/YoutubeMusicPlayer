using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoutubeMusicPlayer.MusicDownloading.Domain;

namespace YoutubeMusicPlayer.MusicDownloading.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicDownloadingDbContext _dbContext;

        public SongRepository(MusicDownloadingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Song song)
        {
            await _dbContext.Songs.AddAsync(song);
        }

        public async Task<bool> Exists(string youtubeId)
        {
            return !(await _dbContext.Songs.FirstOrDefaultAsync(x => x.YtId == youtubeId) is null);
        }
    }
}