using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public class MusicDownloader : IMusicDownloader
    {
        private readonly IFileManager _fileManager;

        private readonly IDownloadService _downloadService;

        public event EventHandler<int> OnProgressChanged;

        public MusicDownloader(IFileManager fileManager,IDownloadService downloadService)
        {
            _fileManager = fileManager;

            _downloadService = downloadService;

            _downloadService.OnProgressChanged += (o, v) => OnProgressChanged(o, v);
        }

        

        public async Task<string> DowloadFileAsync(Music music)
        {         
            var downloadedFileStream=await _downloadService.DownloadMusicAsync(music.VideoId,music);

            var filePath=await _fileManager.CreateFileAsync(music, downloadedFileStream);      

             return filePath;
        }

    }
}