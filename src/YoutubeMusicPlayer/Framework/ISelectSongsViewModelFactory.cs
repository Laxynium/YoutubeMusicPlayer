using Rg.Plugins.Popup.Pages;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.Framework
{
    public interface ISelectSongsViewModelFactory
    {
        T Create<T>(PlaylistViewModel playlistViewModel) where T:ViewModelBase, new();
    }
}