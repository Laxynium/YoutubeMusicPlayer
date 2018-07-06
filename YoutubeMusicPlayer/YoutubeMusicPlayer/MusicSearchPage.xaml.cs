using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicSearchPage : ContentPage
    {
        protected override void OnAppearing()
        {
            //_viewModel.LoadMusicCommand.Execute(null);
        }

        private readonly MusicSearchViewModel _viewModel;
        public MusicSearchPage(MusicSearchViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }
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