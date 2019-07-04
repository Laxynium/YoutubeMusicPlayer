using System;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public interface ISongDownloader
    {
        Task<SongDto> DownloadMusic(string youtubeId);
    }
}