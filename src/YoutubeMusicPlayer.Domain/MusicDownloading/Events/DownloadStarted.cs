using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicDownloading.Events
{
    public class DownloadStarted : IEvent
    {
        public string YoutubeId { get; }
        public string Title { get; }
        public string ImageSource { get; }

        public DownloadStarted(string youtubeId, string title, string imageSource)
        {
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
        }
    }
}