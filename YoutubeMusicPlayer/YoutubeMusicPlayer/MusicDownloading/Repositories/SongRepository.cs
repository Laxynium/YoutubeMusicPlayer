using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;

namespace YoutubeMusicPlayer.MusicDownloading.Repositories
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
            await _connection.InsertAsync(song);
        }

        public async Task<Song> GetAsync(Guid musicId) 
            => await _connection.FindAsync<Song>(musicId);

        public async Task<IEnumerable<Song>> GetAllAsync() 
            => await _connection.Table<Song>().ToListAsync();

        public async Task<bool> Exists(string youtubeId) 
            => await _connection.FindAsync<Song>(x=>x.YoutubeId == youtubeId) != null;

        public async Task RemoveAsync(Guid musicId)
            => await _connection.DeleteAsync<Song>(musicId);
    }
}