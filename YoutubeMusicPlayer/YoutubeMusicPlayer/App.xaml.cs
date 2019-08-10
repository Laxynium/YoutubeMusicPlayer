using System.Linq;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeMusicPlayer.MusicDownloading;

using YoutubeMusicPlayer.Persistence;

#if !DEBUG
[assembly:XamlCompilation(XamlCompilationOptions.Compile)]
#else
[assembly: XamlCompilation(XamlCompilationOptions.Skip)]
#endif
namespace YoutubeMusicPlayer
{
    public partial class App : Application
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly IFileManager _fileManager;

        public App(MainPage page, SQLiteAsyncConnection connection, IFileManager fileManager)
        {
            _connection = connection;
            _fileManager = fileManager;
            InitializeComponent();

#if DEBUG
            LiveReload.Init();
#endif

            MainPage = page;
        }


        protected override async void OnStart()
        {
            await Database.Initialize(_connection);
            _fileManager.CreateFolder();
            var files = _fileManager.ListMusicFiles().ToList();
            await Database.Synchronize(_connection, files);
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
