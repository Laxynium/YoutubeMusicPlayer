using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;

[assembly: Dependency(typeof(IMusicPlayer))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IMusicPlayer
    {
        Task SetSource(string fileUrl);
        Task Play();
        Task Pause();
        Task Stop();
    }
}
