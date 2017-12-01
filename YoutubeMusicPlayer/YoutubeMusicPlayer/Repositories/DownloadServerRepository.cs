using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Repositories
{
    public class DownloadServerRepository : IDownloadServerRepository
    {
        private readonly SQLiteAsyncConnection _sqlConnection;

        public DownloadServerRepository(SQLiteAsyncConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task InitializeAsync()
        {
            await _sqlConnection.CreateTableAsync<DownloadServer>();
        }

        public async Task<IEnumerable<DownloadServer>> GetAllAsync()
        {
            return  await _sqlConnection.Table<DownloadServer>().ToListAsync();
        }

        public async Task<DownloadServer> GetAsync(int id)
        {
            return await _sqlConnection.FindAsync<DownloadServer>(x => x.ServerId == id);
        }

        public async Task AddAsync(DownloadServer downloadServer)
        {
            if (await GetAsync(downloadServer.ServerId) != null)
                return;

            await _sqlConnection.InsertAsync(downloadServer);
        }
    }
}