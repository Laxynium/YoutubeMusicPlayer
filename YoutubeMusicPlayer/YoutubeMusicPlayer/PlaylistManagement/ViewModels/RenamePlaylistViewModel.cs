using System;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class RenamePlaylistViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public ICommand Submit { get; }
        public event EventHandler<string> OnSubmit;
        public RenamePlaylistViewModel(string oldName)
        {
            Name = oldName;
            Submit = new Command(
                () => { OnSubmit?.Invoke(this, Name); });
        }
    }
}