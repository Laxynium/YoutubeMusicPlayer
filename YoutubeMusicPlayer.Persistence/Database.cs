using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Persistence.DatabaseModels;

namespace YoutubeMusicPlayer.Persistence
{
    public static class Database
    {
        public static async Task Initialize(SQLiteAsyncConnection connection)
        {
            await connection.CreateTableAsync<SongDb>();
            await connection.CreateTableAsync<PlaylistDb>();
            await connection.CreateTableAsync<PlaylistSongDb>();
        }

        public static async Task Synchronize(SQLiteAsyncConnection connection, List<string> files)
        {
            var songsToRemove = (await connection.Table<SongDb>().Where(x => !files.Contains(x.FilePath)).ToListAsync()).Select(x=>x.Id);
            await connection.Table<PlaylistSongDb>().DeleteAsync(x => songsToRemove.Contains(x.SongId));
            await connection.Table<SongDb>().DeleteAsync(x => !files.Contains(x.FilePath));
        }

        public static async Task Seed(SQLiteAsyncConnection connection)
        {
            var type = typeof(AllSongsPlaylist).ToString();
            if (await connection.FindAsync<PlaylistDb>(x=>x.Type == type) is null)
                await connection.InsertOrReplaceAsync(new PlaylistDb{Id= Guid.NewGuid(), Name = "All", Type = type });
        }
    }
}