﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Ninject;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Repositories
{
    public interface IMusicRepository
    {
        Task InitializeAsync();

        Task<Music> GetAsync(string id);

        Task<IEnumerable<Music>> GetAllAsync();

        Task AddAsync(Music music);

        Task UpdateAsync(Music music);

        Task DeleteAsync(Music music);
    }
}
