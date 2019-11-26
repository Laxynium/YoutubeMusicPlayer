using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.Events
{
    internal class DownloadStarted : IEvent
    {
        public string YoutubeId { get; }
        public string SongTitle { get; }
        public string ImageSource { get; }

        public DownloadStarted(string youtubeId, string songTitle, string imageSource)
        {
            YoutubeId = youtubeId;
            SongTitle = songTitle;
            ImageSource = imageSource;
        }
    }
}