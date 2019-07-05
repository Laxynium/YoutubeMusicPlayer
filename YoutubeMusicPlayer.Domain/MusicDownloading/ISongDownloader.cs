using System;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public interface ISongDownloader
    {
        Task<SongPath> DownloadMusic(string youtubeId, string title);
        event EventHandler<(string ytId, double progress)> OnDownloadProgress;
    }
}