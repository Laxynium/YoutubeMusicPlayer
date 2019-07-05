using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public interface IDownloadLinkGenerator
    {
        event EventHandler<int> OnProgressChanged;
        
        Task<string> GenerateLinkAsync(string musicIdFromYoutube);
    }
}
