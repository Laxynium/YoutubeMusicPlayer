using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;

namespace YoutubeMusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPlayerPage : ContentPage
    {
        private readonly MusicRepository _musicRepository;

        private readonly IMusicPlayer _musicPlayer;

        private ObservableCollection<Music> _musics;

        private bool IsPlaying { get; set; }

        private Music CurrentPlayingMusic { get; set; }

        private bool IsInitialize { get; set; }

        public MusicPlayerPage()
        {
            InitializeComponent();

            _musicRepository = new MusicRepository(DependencyService.Get<ISqlConnection>().GetConnection());
       
            _musicPlayer = DependencyService.Get<IMusicPlayer>();

            _musicPlayer.PlaybackCompleted += NextButton_OnClicked;

            _musicPlayer.ProgressChanged += _musicPlayer_ProgressChanged;

            slider.ProgressArranged += Slider_ProgressArranged;
        }

        private async void Slider_ProgressArranged(object sender, double e)
        {
            await _musicPlayer.SetProgressAsync(e);
        }

        private async void _musicPlayer_ProgressChanged(object sender, int e)
        {
           await Task.Run(() =>
            {
                slider.Value = e;
            });            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!IsInitialize)
            {
                IsInitialize = true;

                await _musicRepository.InitializeAsync();

                _musics=new ObservableCollection<Music>(await _musicRepository.GetAllAsync());

                listView.ItemsSource = _musics;

                listView.SelectedItem = _musics.Any() ? _musics.First() : null;
                        
                return;
            }

            await UpdateMusicAsync();
        }

        private async Task  UpdateMusicAsync()
        {
            if (_musics == null) return;

            var musics =await  _musicRepository.GetAllAsync();
            foreach (var music in musics)
            {
                var mus=_musics.SingleOrDefault(x => x.VideoId == music.VideoId);

                if(mus==null)
                    _musics.Add(music);
            }        
        }

        private async void PrevButton_OnClicked(object sender, EventArgs e)
        {
            var prevIndex = _musics.IndexOf(CurrentPlayingMusic) - 1;

            if (prevIndex < 0) return;

            await _musicPlayer.Stop();
         
            listView.SelectedItem = _musics[prevIndex];
        }   

        private async void PlayPauseButton_OnClicked(object sender, EventArgs e)
        {
            if (!IsPlaying)
            {
                await _musicPlayer.PlayAsync();
                playPauseButton.Text = "Pause";
            }

            else
            {
                await _musicPlayer.Pause();
                playPauseButton.Text = "Play";
            }
                
            IsPlaying = !IsPlaying;
        }

        private async void NextButton_OnClicked(object sender, EventArgs e)
        {
            await _musicPlayer.Stop();

            var nextIndex = (_musics.IndexOf(CurrentPlayingMusic) + 1)%_musics.Count;

            listView.SelectedItem = _musics[nextIndex];
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var music = e.SelectedItem as Music;

            if (music == null) return;

            CurrentPlayingMusic = music;

            IsPlaying = false;

            await _musicPlayer.SetSourceAsync(music.FilePath);

            PlayPauseButton_OnClicked(null, null);
        }
    }
}