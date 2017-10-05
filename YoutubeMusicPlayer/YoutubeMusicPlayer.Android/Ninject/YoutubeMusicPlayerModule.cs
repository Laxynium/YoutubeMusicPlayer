﻿using System;
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
using SQLite;
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

            Bind<App>().ToSelf().InSingletonScope();

            Bind<MainPage>().ToSelf().InSingletonScope();

            Bind<ContentPage>().To<MusicPlayerPage>().InSingletonScope();

            Bind<ContentPage>().To<MusicSearchPage>().InSingletonScope();

            Bind<ContentPage>().To<DownloadsPage>().InSingletonScope();

            Bind<MusicPlayerViewModel>().ToSelf().InSingletonScope();

            Bind<MusicSearchViewModel>().ToSelf().InSingletonScope();
           
            Bind<DownloadViewModel>().ToSelf().InSingletonScope();
          

            //todo refactor this if it is possible     
            Bind<IFileManager>().ToMethod(x=> DependencyService.Get<IFileManager>()).InSingletonScope();
            Bind<IDownloader>().ToMethod(x => DependencyService.Get<IDownloader>()).InSingletonScope();
            Bind<IFileOpener>().ToMethod(x => DependencyService.Get<IFileOpener>()).InSingletonScope();
            Bind<IMusicPlayer>().ToMethod(x => DependencyService.Get<IMusicPlayer>()).InSingletonScope();
            Bind<SQLiteAsyncConnection>().ToMethod(x => DependencyService.Get<ISqlConnection>().GetConnection()).InSingletonScope();

            Bind<IMusicRepository>().To<MusicRepository>().InSingletonScope();
            Bind<IDownloadService>().To<YtMp3DownloadService>().InSingletonScope();
            Bind<IMusicDownloader>().To<MusicDownloader>().InSingletonScope();
            Bind<IPageService>().To<DownloadPageService>().InSingletonScope();
            Bind<IYoutubeService>().To<YoutubeService>().InSingletonScope();

        }
    }
}