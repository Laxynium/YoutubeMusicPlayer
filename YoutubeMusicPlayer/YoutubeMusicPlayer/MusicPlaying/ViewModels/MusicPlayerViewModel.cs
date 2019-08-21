using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Events;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Domain.MusicPlaying;
using YoutubeMusicPlayer.Domain.SharedKernel;
using YoutubeMusicPlayer.Framework;
using ICommand = System.Windows.Input.ICommand;

namespace YoutubeMusicPlayer.MusicPlaying.ViewModels
{
    public class MusicPlayerViewModel:ViewModelBase, IEventHandler<SongCreated>
    {
        private const string PlayTag = "Play";
        private const string PauseTag = "Pause";

        private SongViewModel _currentSong;

        private readonly IMusicPlayer _musicPlayer;
        private readonly ISongService _songService;

        private readonly ISongRepository _songRepository;

        private bool IsPlaying { get; set; }

        private SongViewModel _nextMusicToPlay;

        private SongCoordinator _songCoordinator;

        public SongViewModel CurrentSong
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

        private ObservableCollection<SongViewModel> _songs= new ObservableCollection<SongViewModel>();

        public ObservableCollection<SongViewModel> Songs
        {
            get => _songs;
            set => SetValue(ref _songs, value);
        }


        private double _musicTimestamp;
        private bool _isInitialized;

        public double MusicTimestamp
        {
            get => _musicTimestamp;
            set => SetValue(ref _musicTimestamp, value);
        }

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

            _musicPlayer.ProgressChanged += OnSongProgressChanged;

            SelectSongCommand = new Command<SongViewModel>(async(m)=>await SelectMusic(m));
            PreviousSongCommand = new Command(PreviousSong);
            NextSongCommand = new Command(NextSong);
            PlayPauseSongCommand = new Command(async()=>await StartPauseSong());
            SetMusicPositionCommand = new Command(async()=>await SetMusicPosition());
            UpdateDataCommand = new Command(async()=>await UpdateData());
            DeleteSongCommand= new Command<SongViewModel>(async (x)=>await DeleteSong(x));
        }

        public Task HandleAsync(SongCreated @event)
        {
            Songs.Add(new SongViewModel(@event.Id,@event.Title,@event.ImageSource,@event.FilePath));
            return Task.CompletedTask;
        }

        private async void OnSongProgressChanged(object sender, int e)
        {
            await Task.Run(() =>
            {
                MusicTimestamp = e;
            });
        }

        private async Task UpdateData()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                var songs = (await _songRepository.GetAllAsync())//TODO think about ViewModel data directly from database
                    .Select((x) => new SongViewModel(x.Id, x.Title, x.ImageSource, x.SongPath)).ToList();

                songs.ForEach(x =>
                {
                    if (Songs.SingleOrDefault(y => y.Id == x.Id) is null)
                        Songs.Add(x);
                });

                _songCoordinator = new SongCoordinator(songs.Select(x=>SongId.FromGuid(x.Id)));

                if(Songs.Count != 0)
                    CurrentSong = Songs.First(x => x.Id == _songCoordinator.CurrentlySelected);
            }
        }

        private async Task SetMusicPosition()
        {
            await _musicPlayer.SetProgressAsync(MusicTimestamp);
        }

        private  void PreviousSong()
        {
            _songCoordinator.GoToPrevious();

            CurrentSong = Songs.First(x => x.Id == _songCoordinator.CurrentlySelected);
        }

        private void NextSong()
        {
            _songCoordinator.GoToNext();

            CurrentSong = Songs.First(x => x.Id == _songCoordinator.CurrentlySelected);
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

        private async Task SelectMusic(SongViewModel selectedMusic)
        {
            if (selectedMusic == null) return;

            CurrentSong = selectedMusic;

            IsPlaying = false;

            await _musicPlayer.Stop();

            await _musicPlayer.SetSourceAsync(selectedMusic.FilePath);

            await StartPauseSong();
        }

        private async Task DeleteSong(SongViewModel music)
        {
            if (CurrentSong.Id == music.Id && IsPlaying)
            {
                NextSong();
            }

            Songs.Remove(music);

            await _songService.RemoveMusic(music.Id);
        }
    }
}
