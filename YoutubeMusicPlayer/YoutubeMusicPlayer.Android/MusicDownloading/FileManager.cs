using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Droid.MusicDownloading;
using YoutubeMusicPlayer.MusicDownloading;

[assembly:Dependency(typeof(FileManager))]
namespace YoutubeMusicPlayer.Droid.MusicDownloading
{
    public class FileManager:IFileManager
    {
        public string GeneratePath(string fileName)
        {
            fileName = fileName.Replace(" ", "").Replace(":", "");

            var folderPath = FolderPath;

            return Path.Combine(folderPath, fileName + ".mp3");
        }

        private static string FolderPath 
            => Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/music";

        public void CreateFolder()
        {
            Directory.CreateDirectory(FolderPath);
        }

        public IEnumerable<string> ListMusicFiles() 
            => Directory.EnumerateFiles(FolderPath).Select(x=>Path.Combine(FolderPath,x));

        public async Task<string> CreateFileAsync(string fileName, Stream stream)
        {
            var filePath = GeneratePath(fileName);

            var file = File.Create(filePath);
                     
            await stream.CopyToAsync(file);

            file.Close();

            return filePath;
        }

        public bool Exists(string filePath) => File.Exists(filePath);

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                if (!Exists(filePath)) return false;

                File.Delete(filePath);

                return true;
            });

        }
    }
}