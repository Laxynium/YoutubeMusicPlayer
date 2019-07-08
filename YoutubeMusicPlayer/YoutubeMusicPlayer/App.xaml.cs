using System.Linq;
using SQLite;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.MusicDownloading;

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
            MainPage = page;
        }


        protected override async void OnStart()
        {
            await _connection.CreateTableAsync<Song>();
            _fileManager.CreateFolder();
            var files = _fileManager.ListMusicFiles().ToList();
            await _connection.Table<Song>().DeleteAsync(x => !files.Contains(x.FilePath));
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
