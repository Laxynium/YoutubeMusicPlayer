using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using YoutubeMusicPlayer.MusicDownloading.ViewModels;
using YoutubeMusicPlayer.MusicSearching;

namespace YoutubeMusicPlayer
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(IEnumerable<ContentPage> pages)
        {
            InitializeComponent();
            pages.ToList().ForEach(p=>Children.Add(p));
            CurrentPage = Children.ToList().OfType<MusicSearchPage>().First();
        }
    }
}
