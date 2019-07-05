using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.EventArgs;
using YoutubeMusicPlayer.MessangingCenter;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer.ViewModels
{
    public class DownloadViewModel : ViewModelBase
    {
        private readonly IMusicRepository _musicRepository;
        private readonly ISongService _songService;

        private readonly IMusicDownloader _musicDownloader;
        private readonly ITabbedPageService _tabbedPageService;


        public ICommand DownloadFileCommand;

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

        public DownloadViewModel(IMusicRepository musicRepository, ISongService songService,
            ITabbedPageService tabbedPageService)
        {
            _musicRepository = musicRepository;
            _songService = songService;
            _tabbedPageService = tabbedPageService;


            //DownloadFileCommand = new Command<MusicViewModel>(async (m) => await DownloadFileAsync(m));
            UpdateDataCommand = new Command(async () => await UpdateData());
            SelectItemCommand = new Command<MusicViewModel>(async (m) => await SelectItem(m));
            HideErrorCommand = new Command(() =>
            {
                ErrorOccured = false;
            });

            _songService.OnDownloadStart += OnDownloadStart;
            _songService.OnDownloadProgress += OnProgress;
            _songService.OnDownloadFinished += OnDownloadFinished;
            _songService.OnDownloadFailed += OnDownloadFailed;

            MessagingCenter.Subscribe<MusicSearchViewModel, MusicEventArgs>(this, GlobalNames.DownloadMusic,
                (s, a) => { DownloadFileCommand.Execute(a.Music); });
        }

        private void OnDownloadFailed(object sender, (string ytId, string msg) e)
        {
            Songs.Remove(Songs.Single(x=>x.VideoId == e.ytId));
            ErrorOccured = true;
            ErrorMessage = e.msg;
        }

        private void OnDownloadFinished(object sender, MusicDto music)
        {
            var vM = Songs.Single(x => x.VideoId == music.YoutubeId);
            vM.FilePath = music.FilePath;
        }

        private void OnProgress(object sender, (string ytId, double progress) e)
        {
            var song = Songs.Single(x => x.VideoId == e.ytId);
            song.Value = e.progress;
        }

        private void OnDownloadStart(object sender, (string ytId, string title, string imageSource) e)
        {
            Songs.Add(new MusicViewModel {ImageSource = e.imageSource, Title = e.title, VideoId = e.ytId});
        }

        private async Task UpdateData()
        {
            //ErrorOccured = false;
            await _musicRepository.InitializeAsync();

            var songs = (await _musicRepository.GetAllAsync())
                .ToList().Select((x) => new MusicViewModel
                {
                    FilePath = x.FilePath,
                    VideoId = x.VideoId,
                    Title = x.Title,
                    ImageSource = x.ImageSource,
                    Value = x.Value
                }).ToList();

            foreach (var song in songs)
            {
                if (String.IsNullOrWhiteSpace(song.FilePath))
                    continue;

                if (Songs.SingleOrDefault(m => m.VideoId == song.VideoId) == null)
                    Songs.Add(song);
            }

            foreach (var song in Songs.ToList())
            {
                if (String.IsNullOrWhiteSpace(song.FilePath)) continue;

                if (songs.SingleOrDefault(m => m.VideoId == song.VideoId) == null)
                    Songs.Remove(song);
            }
        }

        private async Task SelectItem(MusicViewModel music)
        {
            SelectedMusic = null;

            if (music?.FilePath == null) return;

            MessagingCenter.Send(this,GlobalNames.MusicSelected,new MusicEventArgs(){Music = music});

            await _tabbedPageService.ChangePage(0);
        }
    }
}