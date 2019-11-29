using System;
using ChinhDo.Transactions.FileManager;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.MusicDownloading.Repositories;
using YoutubeMusicPlayer.MusicDownloading.Services;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class MusicDownloadingStartup
    {
        public static void Initialize(Container container, string databaseFileName, string musicDirectoryPath)
        {
            InitializeIocContainer(container, databaseFileName, musicDirectoryPath);
            InitializeDatabase();
        }

        private static void InitializeIocContainer(Container container, string databaseFileName, string musicDirectoryPath)
        {
            container.Register(
                () =>
                {
                    var builder = new DbContextOptionsBuilder();
                    builder.UseSqlite($"Filename={databaseFileName}");
                    var dbContext = new MusicDownloadingDbContext(builder.Options);
                    dbContext.Database.Migrate();
                    return dbContext;
                },Lifestyle.Scoped);

            container.Register<UnitOfWork>(Lifestyle.Scoped);

            container.Register<IFileManager, TxFileManager>(Lifestyle.Scoped);

            container.Register<ISongRepository, SongRepository>(Lifestyle.Scoped);

            container.Register<ICommandDispatcher, CommandDispatcher>(Lifestyle.Singleton);

            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Singleton);

            container.Register<ScriptIdEncoder>(Lifestyle.Singleton);

            container.RegisterInstance(typeof(MusicDownloadingOptions), new MusicDownloadingOptions(musicDirectoryPath));

            container.RegisterSingleton<YtMp3DownloadLinkGenerator>();

            container.RegisterSingleton<Downloader>();

            container.Collection.Register(typeof(IEventHandler<>), typeof(MusicDownloadingPackage).Assembly);

            container.Register(typeof(ICommandHandler<>), typeof(MusicDownloadingPackage).Assembly, Lifestyle.Scoped);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>), Lifestyle.Scoped);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionScopeCommandHandlerDecorator<>), Lifestyle.Singleton);
        }

        private static void InitializeDatabase()
        {
            
        }
    }
}