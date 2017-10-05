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
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicPlayerPage : ContentPage
    {
        private readonly MusicPlayerViewModel _viewModel;

        public MusicPlayerPage(MusicPlayerViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private void Slider_ProgressArranged(object sender, double e)
        {
            _viewModel.SetMusicPositionCommand.Execute(e);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.UpdateDataCommand.Execute(null);
        }
        
        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.SelectSongCommand.Execute(e.SelectedItem as MusicViewModel);
        }
    }
}