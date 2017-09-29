using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

[assembly:Dependency(typeof(IFileManager))]

namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface IFileManager
    {
        Task<string> CreateFileAsync(Music music,Stream stream);

        bool Exists(string filePath);
        //Task<>

    }
}
