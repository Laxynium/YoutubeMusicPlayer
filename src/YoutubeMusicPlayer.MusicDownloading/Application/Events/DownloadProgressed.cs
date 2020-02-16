using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Events
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