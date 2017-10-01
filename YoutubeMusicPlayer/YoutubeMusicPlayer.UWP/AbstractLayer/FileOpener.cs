using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.UWP.AbstractLayer;

[assembly:Dependency(typeof(FileOpener))]
namespace YoutubeMusicPlayer.UWP.AbstractLayer
{
    public class FileOpener:IFileOpener
    {
        public async Task OpenFileAsync(string filePath)
        {
            Device.OpenUri(new Uri(filePath));
        }
    }
}