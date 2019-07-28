using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using YoutubeMusicPlayer.PlaylistManagement;

namespace YoutubeMusicPlayer.Framework
{
    public interface IPopupNavigation
    {
        // List of PopupPages which are in the scree
        IReadOnlyList<PopupPage> PopupStack { get; }

        // Open a PopupPage
        Task PushAsync(PopupPage page, bool animate = true);

        // Close the last PopupPage int the PopupStack
        Task PopAsync(bool animate = true);

        // Close all PopupPages in the PopupStack
        Task PopAllAsync(bool animate = true);

        // Close an one PopupPage in the PopupStack even if the page is not the last
        Task RemovePageAsync(PopupPage page, bool animate = true);
    }
}