using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.Events
{
    internal class DownloadFailed : IEvent
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