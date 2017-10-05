using System;
using System.Diagnostics;
using System.Linq;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer
{
    public partial class MainPage 
    {
        public MainPage(/*MusicPlayerPage musicPlayerPage, MusicSearchPage musicSearchPage, DownloadsPage downloadsPage*/)
        {
            InitializeComponent();
            var musicPlayerPage = new MusicPlayerPage(new MusicPlayerViewModel(DependencyService.Get<IFileManager>(),
                new MusicRepository(DependencyService.Get<ISqlConnection>().GetConnection()),
                DependencyService.Get<IMusicPlayer>()));

            Children.Add(musicPlayerPage);
            Children.Add(new MusicSearchPage(new MusicSearchViewModel(new YoutubeService(),new DownloadPageService())));
            Children.Add(new DownloadsPage(new DownloadViewModel(new MusicRepository(DependencyService.Get<ISqlConnection>().GetConnection()),
                new MusicDownloader(DependencyService.Get<IFileManager>(),new YtMp3DownloadService()) )));
            CurrentPage = musicPlayerPage;
        }

        public DownloadsPage DownloadsPage => Children.ToList().OfType<DownloadsPage>().First();
    }
}
