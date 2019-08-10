using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.PlaylistManagement
{
    public partial class AddSongsPopupPage : PopupPage
    {
        private readonly AddSongsViewModel _viewModel;

        public AddSongsPopupPage (AddSongsViewModel viewModel)
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