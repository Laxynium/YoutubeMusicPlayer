using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.MusicDownloading;

[assembly: Dependency(typeof(IDownloader))]
namespace YoutubeMusicPlayer.MusicDownloading
{
    public interface IDownloader
    {
        Task<Stream> GetStreamAsync(string url, Action<int> onProgressChanged);
    }
}
