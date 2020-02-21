using System.IO;
using Android.OS;
using SimpleInjector;
using Xamarin.Forms;
using YoutubeMusicPlayer.Application;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading.UI;
using YoutubeMusicPlayer.MusicDownloading.ViewModels;
using YoutubeMusicPlayer.MusicSearching;
using YoutubeMusicPlayer.MusicSearching.ViewModels;

namespace YoutubeMusicPlayer.Droid
{
    public static class Startup
    {
        private static readonly string DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "MusicDownloading.db3");
        private static readonly string MusicDirectoryPath = Environment.ExternalStorageDirectory.AbsolutePath + "/music2";

        public static App InitializeAndGetRoot()
        {
            var container = new Container()
                .SetupApplication(
                    new YoutubeMusicPlayerOptions
                    {
                        MusicDirectoryPath = MusicDirectoryPath,
                        DatabasePath = DbPath
                    },
                    new[] {typeof(App).Assembly}
                );

            SetupApplicationPagesAndViewModels(container);

            container.Verify(VerificationOption.VerifyAndDiagnose);
            
            return container.GetInstance<App>();
        }
        private static void SetupApplicationPagesAndViewModels(Container container)
        {
            container.Register<App>(Lifestyle.Singleton);
            container.Register<MainPage>();
            container.Register<ITabbedPageService,TabbedPageService>(Lifestyle.Singleton);
            container.RegisterSingleton<SongDownloadsStore>();
            container.Register<MusicSearchViewModel>(Lifestyle.Singleton);
            container.Register<DownloadViewModel>(Lifestyle.Singleton);
            container.Register<MusicSearchPage>(Lifestyle.Singleton);
            container.Register<DownloadsPage>(Lifestyle.Singleton);
            container.Collection.Register<ContentPage>(typeof(MusicSearchPage),typeof(DownloadsPage));
        }
    }
}