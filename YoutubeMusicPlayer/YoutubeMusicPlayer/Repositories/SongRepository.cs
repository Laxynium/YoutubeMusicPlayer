using System.Threading.Tasks;
using SQLite;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;

namespace YoutubeMusicPlayer.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public SongRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }
        public async Task AddAsync(Song song)
        {
            await Task.CompletedTask;
        }
    }
}