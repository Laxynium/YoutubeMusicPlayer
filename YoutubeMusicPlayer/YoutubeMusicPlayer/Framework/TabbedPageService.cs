using System.Threading.Tasks;
using Xamarin.Forms;

namespace YoutubeMusicPlayer.Framework
{
    public class TabbedPageService : ITabbedPageService
    {
        public Task ChangePage(int index)
        {
                var mainPage = (Application.Current.MainPage as MainPage);

                if (mainPage == null)
                    return Task.FromResult<object>(null);

                if (index < 0 || index >= mainPage.Children.Count)
                    return Task.FromResult<object>(null);

                mainPage.CurrentPage = mainPage.Children[index];

                return Task.FromResult<object>(null);
        }
    }
}