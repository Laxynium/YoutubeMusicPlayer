using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.MusicDownloading;

[assembly:Dependency(typeof(IFileManager))]

namespace YoutubeMusicPlayer.MusicDownloading
{
    public interface IFileManager //TODO rethink methods names
    {
        void CreateFolder();

        IEnumerable<string> ListMusicFiles();

        Task<string> CreateFileAsync(string fileName, Stream stream);

        bool Exists(string filePath);

        Task<bool> DeleteFileAsync(string filePath);

        string GeneratePath(string fileName);
    }
}
