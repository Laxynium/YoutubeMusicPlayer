using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

[assembly:Dependency(typeof(IDownloader))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IDownloader
    {
        Task<Stream> GetStreamAsync(string url, INotifyProgressChanged onProgressChanged2);
    }
}
