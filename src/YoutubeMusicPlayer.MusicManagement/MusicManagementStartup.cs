using System.Data.Common;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube;
using YoutubeMusicPlayer.MusicManagement.Domain;
using YoutubeMusicPlayer.MusicManagement.Infrastructure;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.EF;
using YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube;

namespace YoutubeMusicPlayer.MusicManagement
{
    public static class MusicManagementStartup
    {
        public static void Initialize(Container container, string connectionString,
            string musicDirectoryPath)
        {
            RunMigrations(connectionString);
            InitializeFilesystem(musicDirectoryPath);
            InitializeIocContainer(container, musicDirectoryPath);
        }

        private static void RunMigrations(string connectionString)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite(connectionString, o=>o.MigrationsAssembly(typeof(MusicManagementStartup).Assembly.FullName).MigrationsHistoryTable("music_management$migration"));
            using var context = new MusicManagementDbContext(builder.Options);
            context.Database.Migrate();
        }

        private static void InitializeFilesystem(string musicDirectoryPath)
        {
            Directory.CreateDirectory(musicDirectoryPath);
        }

        private static void InitializeIocContainer(Container container, string musicDirectoryPath)
        {
            container.Register(
                () =>
                {
                    var builder = new DbContextOptionsBuilder();
                    builder.UseSqlite(container.GetInstance<SqliteConnection>());
                    var transaction = container.GetInstance<DbTransaction>();
                    var dbContext = new MusicManagementDbContext(builder.Options, transaction);
                    return dbContext;
                }, Lifestyle.Scoped);

            container.RegisterInstance(typeof(MusicManagementOptions), new MusicManagementOptions(musicDirectoryPath));
            container.Register<ScriptIdEncoder>(Lifestyle.Singleton);
            container.RegisterSingleton<YtMp3DownloadLinkGenerator>();
            container.RegisterSingleton<Downloader>();
            container.RegisterSingleton<IYoutubeService,YoutubeService>();
            container.RegisterSingleton<IDownloadProgressNotifier,DownloadProgressNotifier>();
            container.Register<IMainPlaylistRepository, MainPlaylistRepository>(Lifestyle.Scoped);
            container.Register(typeof(ICommandHandler<>), typeof(MusicManagementStartup).Assembly, Lifestyle.Scoped);
            container.Register(typeof(IQueryHandler<,>), typeof(MusicManagementStartup).Assembly, Lifestyle.Singleton);
        }
    }
}