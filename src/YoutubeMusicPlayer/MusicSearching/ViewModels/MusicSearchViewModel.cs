using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicBrowsing;
using YoutubeMusicPlayer.MusicDownloading.Application.Commands;
using ICommand = System.Windows.Input.ICommand;

namespace YoutubeMusicPlayer.MusicSearching.ViewModels
{
    public class MusicSearchViewModel:ViewModelBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
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

        public MusicSearchViewModel(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
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

            var foundSongs = await _queryDispatcher.DispatchAsync(new SearchSongQuery(title));
            
            var musicViewModels = new ObservableCollection<MusicViewModel>(foundSongs.Select(music => new MusicViewModel
            {
                Title = music.Title,
                YtVideoId = music.YoutubeId,
                ImageSource = music.ThumbnailUrl
            }));

            MusicListView = musicViewModels;

            IsSearching = false;
        }

        private async void SelectItem(MusicViewModel music)
        {
            if (music == null) return;

            SelectedMusic = null;

            await _commandDispatcher.DispatchAsync(new DownloadSongCommand(Guid.NewGuid(),
                music.YtVideoId,
                music.Title,
                music.ImageSource));
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
