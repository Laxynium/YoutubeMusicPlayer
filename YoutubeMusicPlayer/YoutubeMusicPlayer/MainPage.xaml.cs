using System.Linq;
using Xamarin.Forms;
using YoutubeMusicPlayer.MusicSearching.ViewModels;

namespace YoutubeMusicPlayer
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(ContentPage[]pages)
        {
            InitializeComponent();
            pages.ToList().ForEach(p=>Children.Add(p));
            CurrentPage = Children.ToList().OfType<MusicSearchPage>().First();
        }
    }
}
