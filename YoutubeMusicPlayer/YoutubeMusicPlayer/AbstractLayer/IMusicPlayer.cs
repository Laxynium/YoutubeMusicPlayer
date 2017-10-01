using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

[assembly: Dependency(typeof(IMusicPlayer))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IMusicPlayer:INotifyProgressChanged
    {
        event EventHandler PlaybackCompleted;

        Task SetProgressAsync(double percentOfLength);
        Task SetSourceAsync(string fileUrl);
        Task PlayAsync();
        Task Pause();
        Task Stop();
    }
}
