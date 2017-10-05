using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.MediaManager.Abstractions.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Annotations;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadsPage : ContentPage
    {
        private readonly DownloadViewModel _viewModel;

        public async Task DownloadFileAsync(MusicViewModel music)
        {
            await Task.Run(()=>_viewModel.DownloadFileCommand.Execute(music));
        }

        public DownloadsPage(DownloadViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }
        
        protected override  void OnAppearing()
        {
              base.OnAppearing();
             _viewModel.UpdateDataCommand.Execute(null);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.SelectItemCommand.Execute(e.SelectedItem as MusicViewModel);
        }   
    }
}