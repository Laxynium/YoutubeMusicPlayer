using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.MessangingCenter;

namespace YoutubeMusicPlayer.MusicDownloading.ViewModels
{
    public class DownloadViewModel : ViewModelBase
    {
        private readonly ISongService _songService;
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
            ISongService songService,
            ITabbedPageService tabbedPageService,
            ISongRepository songRepository
        )
        {
            _songService = songService;
            _tabbedPageService = tabbedPageService;
            _songRepository = songRepository;

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
        }

        private void OnDownloadFailed(object sender, (string ytId, string msg) e)
        {
            Songs.Remove(Songs.Single(x=>x.YtVideoId == e.ytId));
            ErrorOccured = true;
            ErrorMessage = e.msg;
        }

        private void OnDownloadFinished(object sender, MusicDto music)
        {
            var vM = Songs.Single(x => x.YtVideoId == music.YoutubeId);
            vM.FilePath = music.FilePath;
        }

        private void OnProgress(object sender, (string ytId, double progress) e)
        {
            var song = Songs.Single(x => x.YtVideoId == e.ytId);
            song.Value = e.progress;
        }

        private void OnDownloadStart(object sender, (string ytId, string title, string imageSource) e)
        {
            Songs.Add(new MusicViewModel {ImageSource = e.imageSource, Title = e.title, YtVideoId = e.ytId});
        }

        private async Task UpdateData()
        {
            var songs = (await _songRepository.GetAllAsync())
                .ToList().Select((x) => new MusicViewModel
                {
                    FilePath = x.FilePath,
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

            if (music?.FilePath == null) return;

            MessagingCenter.Send(this,GlobalNames.MusicSelected, new MusicEventArgs(){Music = music});

            await _tabbedPageService.ChangePage(0);
        }
    }
}