using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer.ViewModels
{
    public class DownloadViewModel:ViewModelBase
    {
        private readonly IMusicRepository _musicRepository;

        private readonly IMusicDownloader _musicDownloader;

        public ICommand DownloadFileCommand;

        public ICommand UpdateDataCommand;

        public ICommand SelectItemCommand;

        public  ObservableCollection<MusicViewModel> Songs { get; set; } 
                        = new ObservableCollection<MusicViewModel>();

        private MusicViewModel _selectedMusic;
        public MusicViewModel SelectedMusic
        {
            get => _selectedMusic;
            set => SetValue(ref _selectedMusic, value);
        }

        public DownloadViewModel(IMusicRepository musicRepository,IMusicDownloader musicDownloader)
        {
            _musicRepository = musicRepository;
            _musicDownloader = musicDownloader;

            DownloadFileCommand = new Command<MusicViewModel>(async (m) => await DownloadFileAsync(m));
            UpdateDataCommand = new Command(async () => await UpdateData());
            SelectItemCommand = new Command<MusicViewModel>(async (m) =>await SelectItem(m));

        }

        private async Task UpdateData()
        {
            await _musicRepository.InitializeAsync();

            var songs = (await _musicRepository.GetAllAsync())
                .ToList().Select((x)=>new MusicViewModel
                {
                    FilePath = x.FilePath,
                    VideoId = x.VideoId,
                    Title = x.Title,
                    ImageSource = x.ImageSource,
                    Value = x.Value                  
                });

            foreach (var song in songs)
            {
                if (Songs.SingleOrDefault(m => m.VideoId == song.VideoId) == null)
                    Songs.Add(song);
            }
        }

        private async Task DownloadFileAsync(MusicViewModel music)
        {
            if (Songs.SingleOrDefault(x => x.VideoId == music.VideoId) != null)
                return;

            Songs.Add(music);

            var song = new Music
            {
                VideoId = music.VideoId,
                Title = music.Title,
                Value = music.Value,
                ImageSource = music.ImageSource
            };

            song.ProgressChanged += (s,v) =>
            {
                music.Value = v/100f;
                song.Value = v/100f;
            };

            var filePath = await _musicDownloader.DownloadFileAsync(song);

            music.FilePath = filePath;

            song.FilePath = filePath;

            await _musicRepository.AddAsync(song);
        }

        private async Task SelectItem(MusicViewModel music)
        {
            if (music?.FilePath==null) return;

             SelectedMusic = null;

            await DependencyService.Get<IFileOpener>().OpenFileAsync(music.FilePath);
        }
    }
}
