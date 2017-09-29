using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!IsInitialize)
            {
                await _musicRepository.Initialize();

                _musics=new ObservableCollection<Music>(await _musicRepository.GetAllAsync());

                listView.ItemsSource = _musics;
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
                await _musicPlayer.Play();
            else
                await _musicPlayer.Pause();

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

            await _musicPlayer.SetSource(music.FilePath);

            PlayPauseButton_OnClicked(null, null);
        }
    }
}