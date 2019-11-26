using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.Events
{
    internal class DownloadProgressed : IEvent
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