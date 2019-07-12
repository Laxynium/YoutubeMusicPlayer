using System;

namespace YoutubeMusicPlayer.MusicPlaying.ViewModels
{
    public class SongViewModel
    {
        public Guid Id { get; }

        public string Title { get; }

        public string ImageSource { get; }

        public string FilePath { get; }

        public SongViewModel(Guid id, string title, string imageSource, string filePath)
        {
            Id = id;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}