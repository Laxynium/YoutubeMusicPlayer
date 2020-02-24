using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Xamarin.Forms;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.UI;
using ICommand = System.Windows.Input.ICommand;

namespace YoutubeMusicPlayer.MusicDownloading.ViewModels
{
    public class DownloadViewModel : ViewModelBase

    {
        private readonly ITabbedPageService _tabbedPageService;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly SongDownloadsStore _downloadsStore;

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
            IQueryDispatcher queryDispatcher,
            SongDownloadsStore downloadsStore
        )
        {
            _tabbedPageService = tabbedPageService;
            _queryDispatcher = queryDispatcher;
            _downloadsStore = downloadsStore;

            UpdateDataCommand = new Command(async () => await UpdateData());
            SelectItemCommand = new Command<MusicViewModel>(async (m) => await SelectItem(m));
            HideErrorCommand = new Command(() => { ErrorOccured = false; });

            SubscribeForDownloadNotifications();
        }

        public void SubscribeForDownloadNotifications()
        {
            _downloadsStore.SongDownloads.Subscribe(Observer.Create((IList<SongDownload> songs) =>
            {
                foreach (var song in songs)
                {
                    var songVm = Songs.TryFirst(s => s.Id == song.SongId);
                    if (songVm.HasValue)
                        songVm.Value.Value = song.Progress / 100D;
                    else
                        Songs.Add(new MusicViewModel { Id = song.SongId, Title = song.Title, Value = song.Progress, ImageSource = song.ThumbnailUrl });
                }
            }));
        }

        private async Task UpdateData()
        {
            await _downloadsStore.Initialize();
            //var songs = new List<MusicViewModel>();
            //    (await _queryDispatcher.DispatchAsync(new GetAllDownloadedSongsQuery()))
            //.ToList().Select((x) => new MusicViewModel
            //{
            //    YtVideoId = x.YtId,
            //    Title = x.Title,
            //    ImageSource = x.ImageSource,
            //    Value = 1D
            //}).ToList();

            //songs.ForEach(x =>
            //{
            //    if (Songs.SingleOrDefault(y => y.YtVideoId == x.YtVideoId) is null)
            //        Songs.Add(x);
            //});
        }

        private async Task SelectItem(MusicViewModel music)
        {
            SelectedMusic = null;

            //if (music?.SongPath == null) return;

            //MessagingCenter.Send(this,GlobalNames.MusicSelected, new MusicEventArgs(){Music = music});

            //await _tabbedPageService.ChangePage(0);
        }
    }
}