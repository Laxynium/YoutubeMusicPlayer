using System;
using System.Diagnostics;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Services;

namespace YoutubeMusicPlayer
{
    public partial class MainPage 
    {
        private readonly IYoutubeService _youtubeService;

        public MainPage()
        {
            _youtubeService = new YoutubeService();

            InitializeComponent();

            
        }


        private async void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {           
            try
            {
                var title = searchBar.Text;
                SetActivityIndicator(true);
                listView.ItemsSource=await _youtubeService.FindMusic(title);
                SetActivityIndicator(false);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }

        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {         
            try
            {
                var music = e.SelectedItem as Music;

                listView.SelectedItem = null;

                if (music == null) return;

                await downloadPage.DownloadFile(music);
                
                

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }         
        }

        private void SetActivityIndicator(bool enable)
        {
            activityIndicator.IsVisible = enable;
            activityIndicator.IsRunning = enable;
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 0)
            {
                listView.ItemsSource = null;
            }
        }
    }
}
