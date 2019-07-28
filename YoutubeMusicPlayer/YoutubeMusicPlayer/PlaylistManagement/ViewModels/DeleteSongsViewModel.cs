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
    public class DeleteSongsViewModel : ViewModelBase
    {
        private readonly PlaylistViewModel _playlistView;
        private readonly IMusicManagementQueryService _queryService;
        public ObservableCollection<SongViewModel> Songs { get; } = new ObservableCollection<SongViewModel>();
        public ICommand ApprovePickedSongs { get; }
        public ICommand Initialize { get; }
        public event EventHandler<IEnumerable<SongViewModel>> OnApprovedDeletedSongs;

        public DeleteSongsViewModel(PlaylistViewModel playlistView, IMusicManagementQueryService queryService)
        {
            _playlistView = playlistView;
            _queryService = queryService;
            ApprovePickedSongs = new Command(OnApprovePickedSongs);
            Initialize = new Command(async() => await OnInitialize());
        }

        private async Task OnInitialize()
        {
            var songs = await _queryService.GetAllSongsFromPlaylist(_playlistView.Id);
            songs.ToList().ForEach(s=>Songs.Add(new SongViewModel(s.SongId,s.Title,s.ImageSource)));
        }


        private void OnApprovePickedSongs()
        {
            OnApprovedDeletedSongs?.Invoke(this, Songs.Where(x=>x.IsPicked).ToList());
        }
    }
}