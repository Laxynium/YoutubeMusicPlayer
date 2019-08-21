﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Modules;
using SQLite;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.Framework;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Domain.MusicSearching;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using YoutubeMusicPlayer.Droid.Framework;
using YoutubeMusicPlayer.Droid.MusicDownloading;
using YoutubeMusicPlayer.Droid.PlaylistManagement;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading;
using YoutubeMusicPlayer.MusicDownloading.ViewModels;
using YoutubeMusicPlayer.MusicPlaying;
using YoutubeMusicPlayer.MusicPlaying.ViewModels;
using YoutubeMusicPlayer.MusicSearching;
using YoutubeMusicPlayer.MusicSearching.ViewModels;
using YoutubeMusicPlayer.PlaylistManagement;
using YoutubeMusicPlayer.PlaylistManagement.Factories;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

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

            Bind<ContentPage>().To<PlaylistManagementPage>().InSingletonScope();

            Bind<ContentPage>().To<MusicSearchPage>().InSingletonScope();

            Bind<ContentPage>().To<DownloadsPage>().InSingletonScope();

            Bind<MusicPlayerViewModel>().ToSelf().InSingletonScope();

            Bind<MusicSearchViewModel>().ToSelf().InSingletonScope();

            Bind<DownloadViewModel>().ToSelf().InSingletonScope();

            Bind<AddSongsPopupPage>().ToSelf().InSingletonScope();

            Bind<PlaylistSongsViewModel>().ToSelf().InSingletonScope();

            Bind<PlaylistManagementViewModel>().ToSelf().InSingletonScope();

            Bind<IFileManager>().ToMethod(x=> DependencyService.Get<IFileManager>()).InSingletonScope();
            Bind<IDownloader>().ToMethod(x => DependencyService.Get<IDownloader>()).InThreadScope();
            Bind<IFileOpener>().ToMethod(x => DependencyService.Get<IFileOpener>()).InSingletonScope();
            Bind<IMusicPlayer>().ToMethod(x => DependencyService.Get<IMusicPlayer>()).InSingletonScope();
            Bind<IMusicLoader>().To<MusicLoader>().InSingletonScope();
            Bind<IScriptIdEncoder>().To<ScriptIdEncoder>().InTransientScope();
            Bind<SQLiteAsyncConnection>().ToMethod(x => DependencyService.Get<ISqlConnection>().GetConnection());

            Bind<IDownloadLinkGenerator>().To<YtMp3DownloadLinkGenerator>().InSingletonScope();
            Bind<ITabbedPageService>().To<TabbedPageService>().InSingletonScope();
            Bind<IPopupNavigation>().To<PopupNavigation>().InSingletonScope();
            Bind<IMusicSearchingService>().To<YoutubeMusicSearchingService>().InSingletonScope();
            Bind<ISongService>().To<SongService>().InSingletonScope();
            Bind<ISongDownloader>().To<SongDownloader>().InSingletonScope();
            Bind<IAddSongsViewModelFactor>().To<AddSongsViewModelFactor>().InSingletonScope();
            Bind<IPlaylistSongsViewModelFactor>().To<PlaylistSongsViewModelFactor>().InSingletonScope();
            Bind<IPlaylistService>().To<PlaylistService>().InSingletonScope();

            Bind<IEventDispatcher>().To<EventDispatcher>().InSingletonScope();
            Bind<ICommandDispatcher>().To<CommandDispatcher>().InSingletonScope();
            RegisterHandlers(typeof(IEventHandler<>));
            RegisterHandlers(typeof(ICommandHandler<>));
        }

        private void RegisterHandlers(Type typeOfHandler)
        {
            var eventHandlers = GetTypesInAssemblies()
                    .Where(x => x.GetInterfaces().Any(t => IsEventHandlerInterfaceOfType(t,typeOfHandler)))
                    .ToList();

            eventHandlers.ForEach(
                t => Bind(typeOfHandler).ToMethod(c => c.Kernel.Get(t)).InSingletonScope()
            );
        }

        
        private List<Type> GetTypesInAssemblies()
        {
            return new List<Type>
            {
                typeof(ICommand),
                typeof(App),
                typeof(MainActivity)
            }
            .SelectMany(t => Assembly.GetAssembly(t).GetTypes()).ToList();
        }


        private static bool IsEventHandlerInterfaceOfType(Type type, Type expectedType)
        {
            if (!type.IsGenericType)
                return false;

            var typeDefinition = type.GetGenericTypeDefinition();

            return typeDefinition == expectedType;
        }
    }
}