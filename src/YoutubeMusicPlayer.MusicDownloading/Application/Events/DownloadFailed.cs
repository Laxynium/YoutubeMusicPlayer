using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Events
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