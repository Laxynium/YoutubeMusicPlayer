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
        Task<string> CreateFileAsync(string fileName, Stream stream);

        bool Exists(string filePath);

        Task<bool> DeleteFileAsync(string filePath);
    }
}
