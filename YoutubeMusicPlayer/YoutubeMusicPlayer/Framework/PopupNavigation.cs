using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace YoutubeMusicPlayer.Framework
{
    public class PopupNavigation : IPopupNavigation
    {
        public async Task PushAsync(PopupPage page, bool animate = true)
        {
            var mainPage = Application.Current.MainPage as MainPage;
            if (mainPage == null)
            {
                await Task.CompletedTask;
                return;
            }

            await mainPage.Navigation.PushPopupAsync(page);
        }

        public async Task PopAsync(bool animate = true)
        {
            var mainPage = Application.Current.MainPage as MainPage;
            if (mainPage == null)
            {
                await Task.CompletedTask;
                return;
            }

            await mainPage.Navigation.PopPopupAsync(animate);
        }
    }
}