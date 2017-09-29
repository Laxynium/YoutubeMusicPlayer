using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.UWP.AbstractLayer;

[assembly:Dependency(typeof(FileManager))]
namespace YoutubeMusicPlayer.UWP.AbstractLayer
{
    public class FileManager:IFileManager
    {
        public async Task<string> CreateFileAsync(Music music, Stream stream)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string filePath)
        {
           return File.Exists(filePath);
        }
    }
}