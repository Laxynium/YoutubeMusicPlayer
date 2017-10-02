using System;
using System.Diagnostics;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Services;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer
{
    public partial class MainPage 
    {
        private readonly MusicSearchViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();

            BindingContext = _viewModel=new MusicSearchViewModel(new YoutubeService(), new DownloadPageService());
        }

        public DownloadsPage DownloadsPage => downloadPage;

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {        
            _viewModel.SelectItemCommand.Execute(e.SelectedItem as MusicViewModel);       
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.TextChangeCommand.Execute(null);
        }
    }
}
