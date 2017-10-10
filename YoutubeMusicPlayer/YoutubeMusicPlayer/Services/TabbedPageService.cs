using System.Threading.Tasks;
using Xamarin.Forms;

namespace YoutubeMusicPlayer.Services
{
    public class TabbedPageService : ITabbedPageService
    {
        public async Task ChangePage(int index)
        {
            //await Task.Run(() =>
            //{
                var mainPage = (Application.Current.MainPage as MainPage);

                if (mainPage == null) return;

                if (index < 0 || index >= mainPage.Children.Count) return;

                mainPage.CurrentPage = mainPage.Children[index];
           // });                     
        }
    }
}