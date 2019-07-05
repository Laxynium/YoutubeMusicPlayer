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
using YoutubeMusicPlayer.EventArgs;
using YoutubeMusicPlayer.MessangingCenter;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer.ViewModels
{
    public class MusicPlayerViewModel:ViewModelBase
    {
        private const string PlayTag = "Play";
        private const string PauseTag = "Pause";

        private MusicViewModel _currentSong;

        private readonly IMusicPlayer _musicPlayer;

        private readonly IMusicRepository _musicRepository;

        private readonly IFileManager _fileManager;

        private bool IsPlaying { get; set; }

        private bool IsInitialized { get; set; }

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

        public ICommand SelectSongCommand { get; private set; }
        public ICommand PreviousSongCommand { get; private set; }
        public ICommand NextSongCommand { get; private set; }
        public ICommand PlayPauseSongCommand { get; private set; }
        public ICommand SetMusicPositionCommand { get; private set; }
        public ICommand UpdateDataCommand { get; private set; }
        public ICommand DeleteSongCommand { get; private set; }



        public MusicPlayerViewModel(IFileManager fileManager,IMusicRepository musicRepository,IMusicPlayer musicPlayer)
        {
            _fileManager = fileManager;

            _musicRepository = musicRepository;

            _musicPlayer = musicPlayer;

            _musicPlayer.PlaybackCompleted += (s, e) => NextSong();

            _musicPlayer.ProgressChanged += _musicPlayer_ProgressChanged;


            SelectSongCommand = new Command<MusicViewModel>(async(m)=>await SelectMusic(m));
            PreviousSongCommand = new Command(PreviousSong);
            NextSongCommand = new Command(NextSong);
            PlayPauseSongCommand = new Command(async()=>await StartPauseSong());
            SetMusicPositionCommand = new Command(async()=>await SetMusicPosition());
            UpdateDataCommand = new Command(async()=>await UpdateData());
            DeleteSongCommand = new Command<object>(async(s)=>await DeleteSong(s as MusicViewModel));
            DeleteSongCommand= new Command<MusicViewModel>(async (x)=>await DeleteSong(x));

            MessagingCenter.Subscribe<DownloadViewModel,MusicEventArgs>(this,GlobalNames.DownloadFinished, 
                async(s, a) =>
                {
                    await UpdateMusicAsync();
                });
            MessagingCenter.Subscribe<DownloadViewModel, MusicEventArgs>(this, GlobalNames.MusicSelected, (s, a) =>
                {
                    _nextMusicToPlay = a.Music;
                });
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
            if (!IsInitialized)
            {
                IsInitialized = true;

                await _musicRepository.InitializeAsync();

                Songs=new ObservableCollection<MusicViewModel>( (await _musicRepository.GetAllAsync()).Select(x=>new MusicViewModel
                {
                    FilePath = x.FilePath,
                    VideoId = x.VideoId,
                    Title = x.Title,
                    Value = x.Value,
                    ImageSource = x.ImageSource
                }));

                CurrentSong = Songs.Any() ? Songs.First() : null;
            }

            if (_nextMusicToPlay != null)
            {
                CurrentSong = Songs.Single(x => x.VideoId == _nextMusicToPlay.VideoId);
                _nextMusicToPlay = null;
            }

        }

        private async Task UpdateMusicAsync()
        {
            if (Songs == null) return;

            var musics = await _musicRepository.GetAllAsync();
            foreach (var music in musics)
            {
                var mus = Songs.SingleOrDefault(x => x.VideoId == music.VideoId);

                if (mus == null)
                    Songs.Add(new MusicViewModel
                    {
                        FilePath = music.FilePath,
                        VideoId = music.VideoId,
                        Title = music.Title,
                        Value = music.Value,
                        ImageSource = music.ImageSource
                    });
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
            if (CurrentSong.VideoId == music.VideoId && IsPlaying)
            {
                NextSong();
            }
               
            var musicToDel = new Music {VideoId = music.VideoId,FilePath = music.FilePath};

            Songs.Remove(music);

            await _musicRepository.DeleteAsync(musicToDel);

            await _fileManager.DeleteFileAsync(TODO);         
        }

    }
}
