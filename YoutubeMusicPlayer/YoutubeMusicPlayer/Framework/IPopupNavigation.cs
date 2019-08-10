using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using YoutubeMusicPlayer.PlaylistManagement;

namespace YoutubeMusicPlayer.Framework
{
    public interface IPopupNavigation
    {
        Task PushAsync(PopupPage page, bool animate = true);

        Task PopAsync(bool animate = true);
    }
}