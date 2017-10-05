using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Ninject.Modules;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Repositories;
using YoutubeMusicPlayer.Services;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer.Droid.Ninject
{
    class YoutubeMusicPlayerModule:NinjectModule
    {
        public override void Load()
        {
            Bind<string>().ToMethod(x => "");

            Bind<App>().ToSelf();

            Bind<MainPage>().ToSelf();

            Bind<ContentPage>().To<MusicPlayerPage>();

            Bind<ContentPage>().To<MusicSearchPage>();

            Bind<ContentPage>().To<DownloadsPage>();

            Bind<MusicPlayerViewModel>().ToSelf();

            Bind<MusicSearchViewModel>().ToSelf();
           
            Bind<DownloadViewModel>().ToSelf();
          

            //todo refactor this if it is possible     
            Bind<IFileManager>().ToMethod(x=> DependencyService.Get<IFileManager>()).InSingletonScope();
            Bind<IDownloader>().ToMethod(x => DependencyService.Get<IDownloader>()).InSingletonScope();
            Bind<IFileOpener>().ToMethod(x => DependencyService.Get<IFileOpener>()).InSingletonScope();
            Bind<IMusicPlayer>().ToMethod(x => DependencyService.Get<IMusicPlayer>()).InSingletonScope();
            Bind<ISqlConnection>().ToMethod(x => DependencyService.Get<ISqlConnection>()).InSingletonScope();

            Bind<IMusicRepository>().To<MusicRepository>();
            Bind<IDownloadService>().To<YtMp3DownloadService>();
            Bind<IMusicDownloader>().To<MusicDownloader>();
            Bind<IPageService>().To<DownloadPageService>();
            Bind<IYoutubeService>().To<YoutubeService>();








        }
    }
}