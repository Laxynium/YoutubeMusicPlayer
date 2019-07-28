using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.PlaylistManagement
{
    public partial class CreatePlaylistPopup : PopupPage
	{
        private readonly CreatePlaylistViewModel _viewModel;

        public CreatePlaylistPopup (CreatePlaylistViewModel viewModel)
        {
            InitializeComponent ();
            BindingContext = _viewModel = viewModel;
        }
	}
}