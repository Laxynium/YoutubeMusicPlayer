using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public interface ISongDownloader
    {
        Task<Stream> DownloadMusic(string youtubeId);
        event EventHandler<(string ytId, double progress)> OnDownloadProgress;
    }
}