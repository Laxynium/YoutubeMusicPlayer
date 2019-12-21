using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.MusicDownloading;


#if !DEBUG
[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
#else
[assembly: XamlCompilation(XamlCompilationOptions.Skip)]
#endif
namespace YoutubeMusicPlayer
{
    public partial class App : Application
    {
        public App(MainPage page)
        {
            InitializeComponent();

            MainPage = page;
        }


        protected override async void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
