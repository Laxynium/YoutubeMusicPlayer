using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer.Services
{
    public interface IPageService
    {
        Task DownloadFileAsync(MusicViewModel music);
    }

    public class DownloadPageService : IPageService
    {
        public DownloadPageService()
        {
            
        }
        public async Task DownloadFileAsync(MusicViewModel music)
        {
            var song = new Music
            {
                Title = music.Title,
                VideoId = music.VideoId,
                FilePath = music.FilePath,
                Value = music.Value,
                ImageSource = music.ImageSource
            };

            var downloadPage = (Application.Current.MainPage as MainPage)?.DownloadsPage;

            await downloadPage?.DownloadFileAsync(song);
        }
    }
}
