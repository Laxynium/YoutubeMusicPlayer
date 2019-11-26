using System;
using System.IO;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public interface IDownloader
    {
        Task<Stream> GetStreamAsync(string url, Action<int> onProgressChanged);
    }
}
