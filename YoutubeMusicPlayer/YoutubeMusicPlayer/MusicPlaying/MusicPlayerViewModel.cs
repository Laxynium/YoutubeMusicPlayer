using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading.ViewModels;

namespace YoutubeMusicPlayer.MusicPlaying
{
    public class MusicPlayerViewModel:ViewModelBase
    {
        private const string PlayTag = "Play";
        private const string PauseTag = "Pause";

        private MusicViewModel _currentSong;

        private readonly IMusicPlayer _musicPlayer;
        private readonly ISongService _songService;

        private readonly ISongRepository _songRepository;

        private bool IsPlaying { get; set; }

        private MusicViewModel _nextMusicToPlay;

        public MusicViewModel CurrentSong
        {
            get => _currentSong;
            set => SetValue(ref _currentSong, value);
        }

        private string _playButtonText= PlayTag;
        public string PlayButtonText
        {
            get => _playButtonText;
            set => SetValue(ref _playButtonText, value);
        }

        private ObservableCollection<MusicViewModel> _songs= new ObservableCollection<MusicViewModel>();

        public ObservableCollection<MusicViewModel> Songs
        {
            get => _songs;
            set => SetValue(ref _songs, value);
        }


        private double _musicTimestamp;
        public double MusicTimestamp
        {
            get => _musicTimestamp;
            set => SetValue(ref _musicTimestamp, value);
        }
        public ICommand InitializeCommand { get; private set; }
        public ICommand SelectSongCommand { get; private set; }
        public ICommand PreviousSongCommand { get; private set; }
        public ICommand NextSongCommand { get; private set; }
        public ICommand PlayPauseSongCommand { get; private set; }
        public ICommand SetMusicPositionCommand { get; private set; }
        public ICommand UpdateDataCommand { get; private set; }
        public ICommand DeleteSongCommand { get; private set; }



        public MusicPlayerViewModel(ISongRepository songRepository, IMusicPlayer musicPlayer, ISongService songService)
        {
            _songRepository = songRepository;

            _musicPlayer = musicPlayer;

            _songService = songService;

            _musicPlayer.PlaybackCompleted += (s, e) => NextSong();

            _musicPlayer.ProgressChanged += _musicPlayer_ProgressChanged;

            InitializeCommand = new Command(async()=> await UpdateData());
            SelectSongCommand = new Command<MusicViewModel>(async(m)=>await SelectMusic(m));
            PreviousSongCommand = new Command(PreviousSong);
            NextSongCommand = new Command(NextSong);
            PlayPauseSongCommand = new Command(async()=>await StartPauseSong());
            SetMusicPositionCommand = new Command(async()=>await SetMusicPosition());
            UpdateDataCommand = new Command(async()=>await UpdateData());
            DeleteSongCommand= new Command<MusicViewModel>(async (x)=>await DeleteSong(x));

            _songService.OnDownloadFinished += OnDownloadFinished;
        }

        private void OnDownloadFinished(object sender, Domain.MusicDownloading.MusicDto e)
        {
            Songs.Add(new MusicViewModel{FilePath = e.FilePath,ImageSource = e.ImageSource, Title = e.Title,Value = 1D, YtVideoId = e.YoutubeId});
        }

        private async void _musicPlayer_ProgressChanged(object sender, int e)
        {
            await Task.Run(() =>
            {
                MusicTimestamp = e;
            });
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
                if (Songs.SingleOrDefault(y => y.YtVideoId == x.YtVideoId) is null)
                    Songs.Add(x);
            });

            CurrentSong = Songs.Any() ? Songs.First() : null;
            if (_nextMusicToPlay != null)
            {
                CurrentSong = Songs.Single(x => x.YtVideoId == _nextMusicToPlay.YtVideoId);
                _nextMusicToPlay = null;
            }
        }

        private async Task SetMusicPosition()
        {
            await _musicPlayer.SetProgressAsync(MusicTimestamp);
        }

        private  void PreviousSong()
        {
            var prevIndex = Songs.IndexOf(CurrentSong) - 1;

            if (prevIndex < 0) return;

            CurrentSong = Songs[prevIndex];
        }

        private void NextSong()
        {
            var nextIndex = (Songs.IndexOf(CurrentSong) + 1) % Songs.Count;

            CurrentSong = Songs[nextIndex];
        }

        private async Task StartPauseSong()
        {
            if (!IsPlaying)
            {
                await _musicPlayer.PlayAsync();
                PlayButtonText = PauseTag;
            }
            else
            {
                await _musicPlayer.Pause();
                PlayButtonText = PlayTag;
            }
            IsPlaying = !IsPlaying;
        }

        private async Task SelectMusic(MusicViewModel selectedMusic)
        {
            if (selectedMusic == null) return;

            CurrentSong = selectedMusic;

            IsPlaying = false;

            await _musicPlayer.Stop();

            await _musicPlayer.SetSourceAsync(selectedMusic.FilePath);

            await StartPauseSong();
        }

        private async Task DeleteSong(MusicViewModel music)
        {
            //if (CurrentSong.YtVideoId == music.YtVideoId && IsPlaying)
            //{
            //    NextSong();
            //}
               
            //var musicToDel = new MusicDto {YoutubeId = music.YtVideoId,FilePath = music.FilePath};

            //Songs.Remove(music);

            //await _songRepository.DeleteAsync(musicToDel);

            //await _fileManager.DeleteFileAsync(musicToDel.FilePath);         
        }

    }
}
