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
using YoutubeMusicPlayer.EventArgs;
using YoutubeMusicPlayer.MessangingCenter;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer.ViewModels
{
    public class MusicSearchViewModel:ViewModelBase
    {
        private readonly IYoutubeService _youtubeService;


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

        public MusicSearchViewModel(IYoutubeService youtubeService)
        {
            _youtubeService = youtubeService;

            MusicSearchCommand = new Command(SearchMusic);
            SelectItemCommand = new Command<MusicViewModel>(SelectItem);
            TextChangeCommand = new Command(ChangeText);
        }

        private async void SearchMusic()
        {
            try
            {
                var title = SearchText;

                if (String.IsNullOrWhiteSpace(title)) return;

                IsSearching = true;

                var foundMusics = await _youtubeService.FindMusicAsync(title);

                var musicViewModels = new ObservableCollection<MusicViewModel>();

                foreach (var music in foundMusics)
                {
                    musicViewModels.Add(new MusicViewModel
                    {
                        Title = music.Title,
                        VideoId = music.VideoId,
                        ImageSource = music.ImageSource
                    });
                }
                MusicListView = musicViewModels;


                IsSearching = false;
            }
            catch(Exception e)
            {
                ;
            }
           
        }

        private async void SelectItem(MusicViewModel music)
        {
            if (music == null) return;

            SelectedMusic = null;

            await Task.Run(
            () =>
            {
                MessagingCenter.Send(this, GlobalNames.DownloadMusic, new MusicEventArgs { Music = music });
            });
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
