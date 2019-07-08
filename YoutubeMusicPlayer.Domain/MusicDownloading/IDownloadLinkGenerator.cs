using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public interface IDownloadLinkGenerator
    {
        Task<string> GenerateLinkAsync(string musicIdFromYoutube);
    }
}
