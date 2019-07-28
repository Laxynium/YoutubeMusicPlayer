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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RenamePlaylistPopup : PopupPage
    {
        private readonly RenamePlaylistViewModel _viewModel;

        public RenamePlaylistPopup (RenamePlaylistViewModel viewModel)
        {
            InitializeComponent ();
            BindingContext = _viewModel = viewModel;
        }
	}
}