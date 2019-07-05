using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.EventArgs;
using YoutubeMusicPlayer.MessangingCenter;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer.ViewModels
{
    public class MusicSearchViewModel:ViewModelBase
    {
        private readonly IYoutubeService _youtubeService;
        private readonly IMusicLoader _musicLoader;
        private readonly ISongService _songService;

        public string ImageSource { get; set; }

        public string Title { get; set; }

        private bool _isSearching;
        public bool IsSearching
        {
            get => _isSearching;
            set => SetValue(ref _isSearching, value);
        }

        public string SearchText { get; set; }

        private ObservableCollection<MusicViewModel> _musicListView = new ObservableCollection<MusicViewModel>();
        public ObservableCollection<MusicViewModel> MusicListView
        {
            get => _musicListView;
            private set => SetValue(ref _musicListView, value);
        }

        private MusicViewModel _selectedMusic;
        public MusicViewModel SelectedMusic
        {
            get => _selectedMusic;
            set => SetValue(ref _selectedMusic, value);
        }

        public ICommand MusicSearchCommand { get; }

        public ICommand SelectItemCommand { get; }

        public ICommand TextChangeCommand { get; }

        public ICommand LoadMusicCommand { get; }
        public MusicSearchViewModel(IYoutubeService youtubeService,IMusicLoader musicLoader, ISongService songService)
        {
            _youtubeService = youtubeService;
            _musicLoader = musicLoader;
            _songService = songService;

            MusicSearchCommand = new Command(SearchMusic);
            SelectItemCommand = new Command<MusicViewModel>(SelectItem);
            TextChangeCommand = new Command(ChangeText);

            LoadMusicCommand = new Command(LoadFiles);
        }

        private async void SearchMusic()
        {

            var title = SearchText;

            if (String.IsNullOrWhiteSpace(title)) 
                return;

            IsSearching = true;

            var foundMusics = await _youtubeService.FindMusicAsync(title);

            var musicViewModels = new ObservableCollection<MusicViewModel>(foundMusics.Select(music => new MusicViewModel
            {
                Title = music.Title,
                VideoId = music.VideoId,
                ImageSource = music.ImageSource
            }));

            MusicListView = musicViewModels;


            IsSearching = false;
        }

        private async void SelectItem(MusicViewModel music)
        {
            if (music == null) return;

            SelectedMusic = null;

            await _songService.DownloadAndSaveMusic(music.VideoId, music.Title, music.ImageSource);
        }

        //TODO this method was used to load music from device, but this required further rethinking
        private async void LoadFiles()
        {
            await _musicLoader.LoadMusic();
        }
        private void ChangeText()
        {
            if (SearchText.Length == 0)
            {
                MusicListView = null;
            }
        }
    }
}
