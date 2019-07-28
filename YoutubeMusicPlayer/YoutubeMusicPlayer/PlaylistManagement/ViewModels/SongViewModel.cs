using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class SongViewModel : ViewModelBase
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string ImageSource { get; set; }

        public bool IsPicked { get; set; }

        public SongViewModel(Guid id, string title, string imageSource)
        {
            Id = id;
            Title = title;
            ImageSource = imageSource;
        }
    }
}