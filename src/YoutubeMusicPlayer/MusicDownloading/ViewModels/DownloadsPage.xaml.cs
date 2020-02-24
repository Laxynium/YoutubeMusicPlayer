using System.Threading.Tasks;
using Xamarin.Forms;

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
        


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Run(()=> _viewModel.UpdateDataCommand.Execute(null));
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Task.Run(()=> _viewModel.SelectItemCommand.Execute(e.SelectedItem as MusicViewModel));
        }
    }
}