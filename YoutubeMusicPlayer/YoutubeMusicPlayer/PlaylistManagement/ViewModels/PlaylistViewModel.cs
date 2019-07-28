using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public PlaylistViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}