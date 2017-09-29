using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using YoutubeMusicPlayer.Models;

[assembly:Dependency(typeof(FileManager))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class FileManager:IFileManager
    {
        public async Task<string> CreateFileAsync(Music music, Stream stream)
        {
            var fileName = music.Title.Replace(" ", "").Replace(":","");

            var folderPath=Android.OS.Environment.ExternalStorageDirectory.AbsolutePath+"/music";


            var filePath = Path.Combine(folderPath, fileName + ".mp3");

            var file = File.Create(filePath);
                     

            await stream.CopyToAsync(file);

            file.Close();

            return filePath;
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}