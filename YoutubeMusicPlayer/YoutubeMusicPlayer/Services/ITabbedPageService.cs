using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YoutubeMusicPlayer.Services
{
    public interface ITabbedPageService
    {
        Task ChangePage(int index);
    }

    public class TabbedPageService : ITabbedPageService
    {
        public async Task ChangePage(int index)
        {

            var mainPage = (Application.Current.MainPage as MainPage);
            if (mainPage == null) return;
            mainPage.CurrentPage = mainPage.Children[0];
          
        }
    }
}
