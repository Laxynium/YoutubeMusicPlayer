using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer.Services
{
    public interface IPageService
    {
        Task DownloadFileAsync(MusicViewModel music);
    }
}
