using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public interface IDownloadService
    {
        event EventHandler<int> OnProgressChanged;
        
        Task<Stream> DownloadMusicAsync(string musicIdFromYoutube,INotifyProgressChanged onProgressChanged);
    }
}
