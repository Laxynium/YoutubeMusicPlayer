﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Events;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.MessangingCenter;
using ICommand = System.Windows.Input.ICommand;

namespace YoutubeMusicPlayer.MusicDownloading.ViewModels
{
    public class DownloadViewModel : ViewModelBase,
        IEventHandler<DownloadStarted>, 
        IEventHandler<DownloadProgressed>,
        IEventHandler<SongCreated>,
        IEventHandler<DownloadFailed>


    {
        private readonly ITabbedPageService _tabbedPageService;
        private readonly ISongRepository _songRepository;

        public ICommand UpdateDataCommand;

        public ICommand SelectItemCommand;

        public ICommand HideErrorCommand { get; private set; }

        public ObservableCollection<MusicViewModel> Songs { get; set; }
            = new ObservableCollection<MusicViewModel>();

        private MusicViewModel _selectedMusic;

        public MusicViewModel SelectedMusic
        {
            get => _selectedMusic;
            set => SetValue(ref _selectedMusic, value);
        }

        private bool _errorOccured;
        public bool ErrorOccured
        {
            get => _errorOccured;
            set => SetValue(ref _errorOccured, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetValue(ref _errorMessage, value);
        }

        public DownloadViewModel(
            ITabbedPageService tabbedPageService,
            ISongRepository songRepository
        )
        {
            _tabbedPageService = tabbedPageService;
            _songRepository = songRepository;

            UpdateDataCommand = new Command(async () => await UpdateData());
            SelectItemCommand = new Command<MusicViewModel>(async (m) => await SelectItem(m));
            HideErrorCommand = new Command(() =>
            {
                ErrorOccured = false;
            });
        }

        public Task HandleAsync(SongCreated @event)
        {
            var vM = Songs.Single(x => x.YtVideoId == @event.YoutubeId);
            vM.SongPath = @event.FilePath;
            return Task.CompletedTask;
        }

        public Task HandleAsync(DownloadProgressed e)
        {
            var song = Songs.Single(x => x.YtVideoId == e.YoutubeId);
            song.Value = e.Progress;
            return Task.CompletedTask;;
        }

        public Task HandleAsync(DownloadStarted e)
        {
            Songs.Add(new MusicViewModel { ImageSource = e.ImageSource, Title = e.Title, YtVideoId = e.YoutubeId });
            return Task.CompletedTask;
        }

        public Task HandleAsync(DownloadFailed e)
        {
            Songs.Remove(Songs.Single(x => x.YtVideoId == e.YoutubeId));
            ErrorOccured = true;
            ErrorMessage = e.Message;
            return Task.CompletedTask;
        }

        private async Task UpdateData()
        {
            var songs = (await _songRepository.GetAllAsync())
                .ToList().Select((x) => new MusicViewModel
                {
                    SongPath = x.SongPath,
                    YtVideoId = x.YoutubeId,
                    Title = x.Title,
                    ImageSource = x.ImageSource,
                    Value = 1D
                }).ToList();
        
            songs.ForEach(x =>
            {
                if(Songs.SingleOrDefault(y=>y.YtVideoId == x.YtVideoId) is null)
                    Songs.Add(x);
            });
        }

        private async Task SelectItem(MusicViewModel music)
        {
            SelectedMusic = null;

            if (music?.SongPath == null) return;

            MessagingCenter.Send(this,GlobalNames.MusicSelected, new MusicEventArgs(){Music = music});

            await _tabbedPageService.ChangePage(0);
        }
    }
}