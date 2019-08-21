using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicSearching;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicSearching.ViewModels
{
    public class MusicSearchViewModel:ViewModelBase
    {
        private readonly IMusicSearchingService _youtubeService;
        private readonly ISongService _songService;

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

        public MusicSearchViewModel(IMusicSearchingService youtubeService, ISongService songService)
        {
            _youtubeService = youtubeService;
            _songService = songService;

            MusicSearchCommand = new Command(SearchMusic);
            SelectItemCommand = new Command<MusicViewModel>(SelectItem);
            TextChangeCommand = new Command(ChangeText);
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
                YtVideoId = music.YoutubeId,
                ImageSource = music.ImageSource
            }));

            MusicListView = musicViewModels;


            IsSearching = false;
        }

        private async void SelectItem(MusicViewModel music)
        {
            if (music == null) return;

            SelectedMusic = null;

            await _songService.DownloadAndCreateSong(music.YtVideoId, music.Title, music.ImageSource);
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
