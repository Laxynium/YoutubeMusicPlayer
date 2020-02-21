using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.MusicDownloading.ViewModels
{
    public class MusicViewModel:ViewModelBase
    {
        public Guid Id { get; set; }


        private double _value;
        public double Value
        {
            get => _value;
            set => SetValue(ref _value, value);
        }

        public string Title { get; set; }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetValue(ref _imageSource, value);
        }
    }
}
