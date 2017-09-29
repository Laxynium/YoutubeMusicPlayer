using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public interface IYoutubeService
    {
        Task<IEnumerable<Music>> FindMusic(string title);
    }
}
