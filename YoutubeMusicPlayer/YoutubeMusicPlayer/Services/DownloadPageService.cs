using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer.Services
{
    public class DownloadPageService : IDownloadPageService
    {
        public DownloadPageService()
        {
            
        }
        public async Task DownloadFileAsync(MusicViewModel music)
        {
            var downloadPage = (Application.Current.MainPage as MainPage)?.DownloadsPage;
            if (downloadPage == null) return;

            await downloadPage?.DownloadFileAsync(music);
        }
    }
}