using System.IO;
using ChinhDo.Transactions.FileManager;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SimpleInjector;
using YoutubeMusicPlayer.Framework.Decorators;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Application;
using YoutubeMusicPlayer.MusicDownloading.Application.Repositories;
using YoutubeMusicPlayer.MusicDownloading.Application.Services;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class MusicDownloadingStartup
    {
        public static void Initialize(Container container, string connectionString, string musicDirectoryPath)
        {
            InitializeIocContainer(container, connectionString, musicDirectoryPath);
            InitializeDatabase(connectionString);
            InitializeFilesystem(musicDirectoryPath);
        }

        private static void InitializeIocContainer(Container container, string connectionString, string musicDirectoryPath)
        {
            container.Register(() => new SqliteConnection(connectionString), Lifestyle.Scoped);
            container.Register(
                () =>
                {
                    var builder = new DbContextOptionsBuilder();
                    builder.UseSqlite(container.GetInstance<SqliteConnection>());
                    var dbContext = new MusicDownloadingDbContext(builder.Options);
                    return dbContext;
                },Lifestyle.Scoped);
            container.Register(()=>new DbContextOptionsBuilder().UseSqlite(connectionString).Options,Lifestyle.Singleton);

            container.Register<DbContext>(container.GetInstance<MusicDownloadingDbContext>, Lifestyle.Scoped);

            container.Register<ISongRepository, SongRepository>(Lifestyle.Scoped);

            container.Register<ScriptIdEncoder>(Lifestyle.Singleton);

            container.RegisterInstance(typeof(MusicDownloadingOptions), new MusicDownloadingOptions(musicDirectoryPath));

            container.RegisterSingleton<YtMp3DownloadLinkGenerator>();

            container.RegisterSingleton<Downloader>();

            container.Register(typeof(ICommandHandler<>), typeof(MusicDownloadingStartup).Assembly, Lifestyle.Scoped);

            //Read model
            container.Register(typeof(IQueryHandler<,>),typeof(MusicDownloadingStartup).Assembly,Lifestyle.Singleton);
        }

        private static void InitializeDatabase(string connectionString)
        {
            DbMigrator.SetupDb(connectionString);
        }

        private static void InitializeFilesystem(string musicDirectoryPath)
        {
            Directory.CreateDirectory(musicDirectoryPath);
        }
    }
}