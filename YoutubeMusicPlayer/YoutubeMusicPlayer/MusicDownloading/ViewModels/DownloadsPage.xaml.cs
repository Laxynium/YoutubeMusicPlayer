using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YoutubeMusicPlayer.MusicDownloading.ViewModels
{
    public partial class DownloadsPage : ContentPage
    {
        private readonly DownloadViewModel _viewModel;

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