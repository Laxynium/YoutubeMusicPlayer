using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.PlaylistManagement
{
    public partial class SelectSongsPopup : PopupPage
    {
        private readonly SelectSongsViewModel _viewModel;

        public SelectSongsPopup (SelectSongsViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent ();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.Initialize?.Execute(null);
        }
    }
}