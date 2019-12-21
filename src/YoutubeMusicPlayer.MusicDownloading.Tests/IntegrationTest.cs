using System.IO;
using ChinhDo.Transactions.FileManager;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicDownloading.Infrastructure;

namespace YoutubeMusicPlayer.MusicDownloading.Tests
{
    public class IntegrationTest
    {
        public ICommandDispatcher Dispatcher { get; private set; }
        public static readonly string DbLocation = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "TestDb.db3");
        public static readonly string ConnectionString = $"Data Source={DbLocation}";
        public static readonly string MusicDirectory = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
            "music");
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
            if(File.Exists(DbLocation))
                File.Delete(DbLocation);
        } 

        private void ClearFiles()
        {
            if(Directory.Exists(MusicDirectory))
                Directory.Delete(MusicDirectory,true);
        }

        private void SetupModules(Container container)
        {
            FrameworkStartup.Initialize(container);
            MusicDownloadingStartup.Initialize(container, ConnectionString, MusicDirectory);
        }
    }
}