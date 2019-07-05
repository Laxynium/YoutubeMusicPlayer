using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

[assembly: Dependency(typeof(IDownloader))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IDownloader
    {
        Task<Stream> GetStreamAsync(string url, Action<int> onProgressChanged);
    }
}
