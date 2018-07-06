using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;

[assembly: Dependency(typeof(IMusicLoader))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IMusicLoader
    {
        Task LoadMusic();
    }
}