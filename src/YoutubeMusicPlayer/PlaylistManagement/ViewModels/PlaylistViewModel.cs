using System;
using System.Windows.Input;
using Xamarin.Forms;
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

        private string _newName;

        public string NewName   
        {
            get => _newName;
            set => SetValue(ref _newName, value);
        }

        private bool _isChangeNameState;
        public bool IsChangeNameState
        {
            get => _isChangeNameState;
            set => SetValue(ref _isChangeNameState, value);
        }

        public ICommand StartRenamingPlaylist { get; }
        public ICommand CancelRenamingPlaylist { get; }

        public PlaylistViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
            StartRenamingPlaylist = new Command(() => IsChangeNameState = true);
            CancelRenamingPlaylist = new Command(() => IsChangeNameState = false);
        }

        public void UpdateName()
        {
            Name = NewName;
            IsChangeNameState = false;
        }
    }
}