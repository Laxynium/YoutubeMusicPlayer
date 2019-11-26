using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.Events
{
    internal class DownloadFinished : IEvent
    {
        public Guid Id { get; }
        public string YoutubeId { get; }
        public string FilePath { get; }
        public string Title { get; }
        public string ImageSource { get; }

        public DownloadFinished(Guid id , string youtubeId, string filePath, string title, string imageSource)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}