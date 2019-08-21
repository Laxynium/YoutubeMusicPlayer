using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Events
{
    public class DownloadProgressed : IEvent
    {
        public string YoutubeId { get; }
        public double Progress { get; }

        public DownloadProgressed(string youtubeId, double progress)
        {
            YoutubeId = youtubeId;
            Progress = progress;
        }
    }
}