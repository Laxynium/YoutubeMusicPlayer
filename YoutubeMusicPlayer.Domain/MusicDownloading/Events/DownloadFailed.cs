using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Events
{
    public class DownloadFailed : IEvent
    {
        public string YoutubeId { get; }
        public string Message { get; }

        public DownloadFailed(string youtubeId, string message)
        {
            YoutubeId = youtubeId;
            Message = message;
        }
    }
}