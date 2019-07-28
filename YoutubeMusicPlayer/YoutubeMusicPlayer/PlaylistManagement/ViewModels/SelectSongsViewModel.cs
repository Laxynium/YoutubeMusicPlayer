using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.MusicManagement.Queries;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class SelectSongsViewModel : ViewModelBase
    {
        private readonly PlaylistViewModel _playlistView;
        private readonly IMusicManagementQueryService _queryService;
        public ObservableCollection<SongViewModel> Songs { get; } = new ObservableCollection<SongViewModel>();
        public ICommand ApprovePickedSongs { get; }
        public ICommand Initialize { get; }
        public event EventHandler<IEnumerable<SongViewModel>> OnApprovedPickedSongs;

        public SelectSongsViewModel(PlaylistViewModel playlistView, IMusicManagementQueryService queryService)
        {
            _playlistView = playlistView;
            _queryService = queryService;
            ApprovePickedSongs = new Command(OnApprovePickedSongs);
            Initialize = new Command(async() => await OnInitialize());
        }

        private async Task OnInitialize()
        {
            var songs = await _queryService.GetAllMusicNotOnPlaylist(_playlistView.Id);
            songs.ToList().ForEach(s=>Songs.Add(new SongViewModel(s.SongId,s.Title,s.ImageSource)));
        }


        private void OnApprovePickedSongs()
        {
            OnApprovedPickedSongs?.Invoke(this, Songs.Where(x=>x.IsPicked).ToList());
        }
    }
}