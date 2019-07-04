using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.MusicDownloading;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class SongDownloader : ISongDownloader
    {
        public SongDownloader()
        {
            
        }
        public async Task<SongDto> DownloadMusic(string youtubeId)
        {
            return await Task.FromResult(null as SongDto);
        }
    }
}