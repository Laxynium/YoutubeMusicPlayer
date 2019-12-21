using Rg.Plugins.Popup.Pages;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.PlaylistManagement
{
    public partial class PlaylistSongsPopupPage : PopupPage
	{
        private readonly PlaylistSongsViewModel _viewModel;

        public PlaylistSongsPopupPage (PlaylistSongsViewModel viewModel)
        {
            InitializeComponent ();
            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            _viewModel.Initialize?.Execute(null);
        }
    }
}