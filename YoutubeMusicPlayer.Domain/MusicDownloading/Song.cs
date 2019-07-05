using System;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class Song
    {
        public Guid Id { get; }
        public string YoutubeId { get; }
        public string Title { get; }
        public double ProgressValue { get; private set; } = 0;
        public string ImageSource { get; private set; }
        public string FilePath { get; private set; }

        public Song(string youtubeId, string title, string imageSource, string filePath)
        {
            Id = Guid.NewGuid();
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }

        public Song(Guid id, string youtubeId, string title, string imageSource, string filePath)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }


        public void UpdateProgress(double newProgress)
        {
            if(newProgress < 0 || newProgress > 1)
                throw new ArgumentException($"Invalid value of {newProgress}.");

            ProgressValue = newProgress;
        }
    }
}