using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.ViewModels;

namespace YoutubeMusicPlayer.Services
{
    public class DownloadPageService : IPageService
    {
        public DownloadPageService()
        {
            
        }
        public async Task DownloadFileAsync(MusicViewModel music)
        {
            var downloadPage = (Application.Current.MainPage as MainPage)?.DownloadsPage;

            await downloadPage?.DownloadFileAsync(music);
        }
    }
}