using Xamarin.Forms;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.PlaylistManagement
{
    public partial class PlaylistManagementPage : ContentPage
	{
		private readonly PlaylistManagementViewModel _playlistManagementViewModel;

	   

		public PlaylistManagementPage (PlaylistManagementViewModel playlistManagementViewModel)
		{
			_playlistManagementViewModel = playlistManagementViewModel;
			InitializeComponent ();
			BindingContext = _playlistManagementViewModel = playlistManagementViewModel;
		}

		private void OnPlaylistSelected(object sender, SelectedItemChangedEventArgs e)
		{
		}

        protected override void OnAppearing()
        {
            _playlistManagementViewModel.Initialize?.Execute(null);
        }
    }
}