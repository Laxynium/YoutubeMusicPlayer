using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicPlaying.ViewModels
{
    public class SongViewModel : ViewModelBase
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetValue(ref _imageSource, value);
        }

        public string FilePath { get; set; }

        public SongViewModel()
        {
            
        }
        public SongViewModel(Guid id, string title, string imageSource, string filePath)
        {
            Id = id;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}