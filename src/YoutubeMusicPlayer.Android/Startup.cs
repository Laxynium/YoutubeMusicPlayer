using System.IO;
using Android.OS;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Xamarin.Forms;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicBrowsing;
using YoutubeMusicPlayer.MusicDownloading;
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
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;

            SetupModules(container);

            container.Verify(VerificationOption.VerifyAndDiagnose);

            return container.GetInstance<App>();
        }

        private static void SetupModules(Container container)
        {
            FrameworkStartup.Initialize(container);
            MusicDownloadingStartup.Initialize(container, $"Data Source={DbPath};", MusicDirectoryPath);
            MusicBrowsingStartup.Initialize(container);

            SetupApplicationPagesAndViewModels(container);
        }

        private static void SetupApplicationPagesAndViewModels(Container container)
        {
            //var repositoryAssembly = typeof(App).Assembly;
            //var registrations = repositoryAssembly.GetExportedTypes()
            //    .Where(type => type.Namespace.StartsWith("YoutubeMusicPlayer"))
            //    .SelectMany(type => type.GetInterfaces(), (type, service) => new {service, implementation = type});
            //foreach (var reg in registrations)
            //{
            //    container.Register(reg.service, reg.implementation, Lifestyle.Singleton);
            //}
            container.Register<App>(Lifestyle.Singleton);
            container.Register<MainPage>();
            container.Register<ITabbedPageService,TabbedPageService>(Lifestyle.Singleton);
            container.Register<MusicSearchViewModel>(Lifestyle.Singleton);
            container.Register<DownloadViewModel>(Lifestyle.Singleton);
            container.Register<MusicSearchPage>(Lifestyle.Singleton);
            container.Register<DownloadsPage>(Lifestyle.Singleton);
            container.Collection.Register<ContentPage>(typeof(MusicSearchPage),typeof(DownloadsPage));
            container.Collection.Register(typeof(IEventHandler<>), typeof(App).Assembly,typeof(MusicDownloadingStartup).Assembly);
        }
    }
}