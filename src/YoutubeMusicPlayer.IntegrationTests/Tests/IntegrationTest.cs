using System;
using System.IO;
using ChinhDo.Transactions.FileManager;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.IntegrationTests.Tests
{
    public class IntegrationTest
    {
        public ICommandDispatcher Dispatcher { get; private set; }
        public static readonly string DbLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestDb.db3");
        public static readonly string ConnectionString = $"Data Source={DbLocation}";
        public static readonly string MusicDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "testMusic");
        public IntegrationTest()
        {
            ClearDatabase();
            ClearFiles();
            SetupDispatcher();
        }

        private void SetupDispatcher()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;
            SetupModules(container);

            container.Verify(VerificationOption.VerifyAndDiagnose);
            Dispatcher = container.GetInstance<ICommandDispatcher>();
        }

        private void ClearDatabase()
        {
            var fileManager = new TxFileManager();
            if(fileManager.FileExists(DbLocation))
                fileManager.Delete(DbLocation);
        } 

        private void ClearFiles()
        {
            var fileManager = new TxFileManager();
            if(fileManager.DirectoryExists(MusicDirectory))
                fileManager.DeleteDirectory(MusicDirectory);
        }

        private void SetupModules(Container container)
        {
            FrameworkStartup.Initialize(container);
            MusicDownloadingStartup.Initialize(container, ConnectionString, MusicDirectory);
        }
    }
}