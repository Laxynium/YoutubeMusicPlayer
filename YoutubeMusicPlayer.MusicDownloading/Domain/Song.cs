using System;

namespace YoutubeMusicPlayer.MusicDownloading.Domain
{
    public class Song
    {
        public string FilePath { get; }
        public string YtId { get; }
        public string Title { get; }
        public string ImageSource { get; }
        public Guid Id { get; }

        public Song(string filePath, string ytId, string title, string imageSource)
        {
            FilePath = filePath;
            YtId = ytId;
            Title = title;
            ImageSource = imageSource;
            Id = Guid.NewGuid();
        }
    }
}