using System.Collections.Generic;
using System.Threading.Tasks;
using PCLStorage;
using SQLite;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Repositories
{
    public class MusicRepository:IMusicRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public MusicRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;           
        }

        public async Task InitializeAsync()
        {
            await _connection.CreateTableAsync<Music>();

            var musics = await GetAllAsync();

            foreach (var music in musics)
            {
                if (!DependencyService.Get<IFileManager>().Exists(music.FilePath))
                {
                    await DeleteAsync(music);
                }
            }
        }

        public async Task<Music> GetAsync(string id)
        {          
            return await _connection.GetAsync<Music>(x => x.VideoId == id);
        }

        public async Task<IEnumerable<Music>> GetAllAsync()
        {
            return await _connection.Table<Music>().ToListAsync();
        }

        public async Task AddAsync(Music music)
        {
            await _connection.InsertAsync(music);
        }

        public async Task UpdateAsync(Music music)
        {
            await _connection.UpdateAsync(music);
        }

        public async Task DeleteAsync(Music music)
        {
            await _connection.DeleteAsync(music);
        }

    }
}