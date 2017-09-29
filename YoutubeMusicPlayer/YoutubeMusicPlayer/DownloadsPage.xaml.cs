using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadsPage : ContentPage
    {
        private readonly ObservableCollection<Music>_collection=new ObservableCollection<Music>();

        private readonly IMusicRepository _musicRepository;

        private readonly MusicDownloader _musicDownloader;

        public async Task DownloadFile(Music music)
        {
            if (_collection.SingleOrDefault(x => x.VideoId == music.VideoId) != null)
            {
                return;
            }

            _collection.Add(music);

            var filePath=await _musicDownloader.DowloadFile(music);

            music.FilePath = filePath;

            await _musicRepository.AddAsync(music);
        }

        public DownloadsPage()
        {
            InitializeComponent();

            _musicRepository = new MusicRepository(DependencyService.Get<ISqlConnection>().GetConnection());

            _musicDownloader = new MusicDownloader(DependencyService.Get<IFileManager>(), new YtMp3DownloadService());

            listView.ItemsSource = _collection;
        }
        
        protected override async void OnAppearing()
        {
            await _musicRepository.Initialize();

            var musics = await _musicRepository.GetAllAsync();
            
            foreach (var music in musics)
            {
                if(_collection.SingleOrDefault(m=>m.VideoId==music.VideoId)==null)
                    _collection.Add(music);
            }
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var downloadItem = e.SelectedItem as Music;

            listView.SelectedItem = null;

            if (downloadItem?.FilePath == null) return;
      
            await DependencyService.Get<IFileOpener>().OpenFile(downloadItem.FilePath);
        }

       
    }
}