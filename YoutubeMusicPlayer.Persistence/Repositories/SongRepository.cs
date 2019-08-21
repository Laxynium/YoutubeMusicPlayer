using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Domain.SharedKernel;
using YoutubeMusicPlayer.Persistence.DatabaseModels;

namespace YoutubeMusicPlayer.Persistence.Repositories
{
    internal class SongRepository : ISongRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public SongRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public async Task AddAsync(Song song)
        {
            await _connection.InsertAsync(FromSong(song));
        }

        public async Task<Song> GetAsync(SongId musicId)
            => ToSong(await _connection.FindAsync<SongDb>(musicId.Value));

        public async Task<IEnumerable<Song>> GetAllAsync()
            => (await _connection.Table<SongDb>().ToListAsync()).Select(ToSong);

        public async Task<bool> Exists(string youtubeId)
            => await _connection.FindAsync<SongDb>(x => x.YoutubeId == youtubeId) != null;

        public async Task RemoveAsync(SongId musicId)
        {
            await _connection.RunInTransactionAsync(c =>
            {
                c.Delete<SongDb>(musicId.Value);
                c.Table<PlaylistSongDb>().Delete(x => x.SongId == musicId.Value);
            });
        }

        private static SongDb FromSong(Song song)
            => new SongDb
            {
                Id = song.Id,
                YoutubeId = song.YoutubeId,
                Title = song.Title,
                ImageSource = song.ImageSource,
                FilePath = song.SongPath
            };
        private static Song ToSong(SongDb db)
            => new Song(SongId.FromGuid(db.Id), new SongPath(db.FilePath), db.YoutubeId, db.Title, db.ImageSource);
    }
}