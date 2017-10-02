using System;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public interface IMusicDownloader
    {
        event EventHandler<int> OnProgressChanged;

        Task<string> DownloadFileAsync(Music music);
    }
}
