using Ninject.Modules;
using SQLite;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Domain.MusicSearching;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using YoutubeMusicPlayer.Droid.MusicDownloading;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading;
using YoutubeMusicPlayer.MusicDownloading.Repositories;
using YoutubeMusicPlayer.MusicDownloading.ViewModels;
using YoutubeMusicPlayer.MusicPlaying;
using YoutubeMusicPlayer.MusicPlaying.ViewModels;
using YoutubeMusicPlayer.MusicSearching;
using YoutubeMusicPlayer.MusicSearching.ViewModels;

namespace YoutubeMusicPlayer.Droid.Ninject
{
    class YoutubeMusicPlayerModule:NinjectModule
    {
        public override void Load()
        {
            Bind<string>().ToMethod(x => "");

            Bind<App>().ToSelf().InSingletonScope();

            Bind<MainPage>().ToSelf().InSingletonScope();

            Bind<ContentPage>().To<MusicPlayerPage>().InSingletonScope();

            Bind<ContentPage>().To<MusicSearchPage>().InSingletonScope();

            Bind<ContentPage>().To<DownloadsPage>().InSingletonScope();

            Bind<MusicPlayerViewModel>().ToSelf().InSingletonScope();

            Bind<MusicSearchViewModel>().ToSelf().InSingletonScope();
           
            Bind<DownloadViewModel>().ToSelf().InSingletonScope();
          
            //todo refactor this if it is possible, because below mappings are similar 
            Bind<IFileManager>().ToMethod(x=> DependencyService.Get<IFileManager>()).InSingletonScope();
            Bind<IDownloader>().ToMethod(x => DependencyService.Get<IDownloader>()).InThreadScope();
            Bind<IFileOpener>().ToMethod(x => DependencyService.Get<IFileOpener>()).InSingletonScope();
            Bind<IMusicPlayer>().ToMethod(x => DependencyService.Get<IMusicPlayer>()).InSingletonScope();
            Bind<IMusicLoader>().To<MusicLoader>().InSingletonScope();
            Bind<IScriptIdEncoder>().To<ScriptIdEncoder>().InTransientScope();
            Bind<SQLiteAsyncConnection>().ToMethod(x => DependencyService.Get<ISqlConnection>().GetConnection());

            Bind<ISongRepository>().To<SongRepository>().InSingletonScope();
            Bind<IDownloadLinkGenerator>().To<YtMp3DownloadLinkGenerator>().InSingletonScope();
            Bind<ITabbedPageService>().To<TabbedPageService>().InSingletonScope();
            Bind<IMusicSearchingService>().To<YoutubeMusicSearchingService>().InSingletonScope();
            Bind<ISongService>().To<SongService>().InSingletonScope();
            Bind<ISongDownloader>().To<SongDownloader>().InSingletonScope();
        }
    }
}