using System;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        public Guid Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public PlaylistViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}