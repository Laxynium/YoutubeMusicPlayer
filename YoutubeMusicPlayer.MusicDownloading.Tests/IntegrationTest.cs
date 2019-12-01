using ChinhDo.Transactions.FileManager;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.Tests
{
    public class IntegrationTest
    {
        public ICommandDispatcher Dispatcher { get; private set; }
        public static readonly string DbLocation = "TestDb.db";
        public static readonly string ConnectionString = $"Data Source={DbLocation}";
        public static readonly string MusicDirectory = "music";
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
            MusicDownloadingStartup.Initialize(container, ConnectionString, MusicDirectory);
        }
    }
}