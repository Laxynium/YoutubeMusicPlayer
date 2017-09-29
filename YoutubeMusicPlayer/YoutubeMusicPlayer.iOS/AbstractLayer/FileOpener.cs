using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.iOS.AbstractLayer;

[assembly:Dependency(typeof(FileOpener))]
namespace YoutubeMusicPlayer.iOS.AbstractLayer
{
    public class FileOpener:IFileOpener
    {
        public async Task OpenFile(string filePath)
        {
            Device.OpenUri(new Uri(filePath));
        }
    }
}