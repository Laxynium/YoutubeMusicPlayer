using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;

[assembly:Dependency(typeof(IFileOpener))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IFileOpener
    {
        Task OpenFile(string filePath);
    }
}
