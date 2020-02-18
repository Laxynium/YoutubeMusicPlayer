using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using YoutubeMusicPlayer.Application;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class IntegrationTest
    {
        protected readonly Container Container;
        protected ICommandDispatcher CommandDispatcher => Container.GetInstance<ICommandDispatcher>();
        protected IQueryDispatcher QueryDispatcher => Container.GetInstance<IQueryDispatcher>();

        protected static readonly string DbLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestingDatabase.db3");
        protected static readonly string MusicDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "testingMusicDirectory");
        protected DbContextOptions ContextOptions = new DbContextOptionsBuilder().UseSqlite($"Data Source={DbLocation}").Options;
        protected IntegrationTest()
        {
            ClearDatabase();
            ClearFiles();
            Container = SetupContainer();
        }

        private static Container SetupContainer()
        {
            var container = new Container();
            container.SetupApplication(
                new YoutubeMusicPlayerOptions {MusicDirectoryPath = MusicDirectory, DatabasePath = DbLocation}
            );
            container.Verify(VerificationOption.VerifyAndDiagnose);
            return container;
        }

        private void ClearDatabase()
        {
            if(File.Exists(DbLocation))
                File.Delete(DbLocation);
        } 

        private void ClearFiles()
        {
            if(Directory.Exists(MusicDirectory))
                Directory.Delete(MusicDirectory,true);
        }
    }
}