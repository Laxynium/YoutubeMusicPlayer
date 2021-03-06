﻿using System.Collections.Generic;
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
            var music= await _connection.FindAsync<Music>(x => x.VideoId == id);
            return music;
        }

        public async Task<IEnumerable<Music>> GetAllAsync()
        {
            return await _connection.Table<Music>().ToListAsync();
        }

        public async Task AddAsync(Music music)
        {
            if (await GetAsync(music.VideoId) != null)
                return;

            await _connection.InsertAsync(music);
        }

        public async Task UpdateAsync(Music music)
        {
            var musicInDb = await GetAsync(music.VideoId);
            musicInDb.FilePath = music.FilePath;
            musicInDb.ImageSource = music.ImageSource;
            musicInDb.Title = music.Title;
            musicInDb.Value = music.Value;
            await _connection.UpdateAsync(musicInDb);
        }

        public async Task DeleteAsync(Music music)
        {
            var musicInDb=await GetAsync(music.VideoId);

            await _connection.DeleteAsync(musicInDb);
        }
    }
}