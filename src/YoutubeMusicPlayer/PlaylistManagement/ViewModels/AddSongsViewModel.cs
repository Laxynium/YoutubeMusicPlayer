using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class AddSongsViewModel : ViewModelBase
    {
        private readonly PlaylistViewModel _playlistView;
        //private readonly IMusicManagementQueryService _queryService;
        //private readonly IPlaylistService _playlistService;
        private readonly IPopupNavigation _navigation;
        public ObservableCollection<SongViewModel> Songs { get; } = new ObservableCollection<SongViewModel>();
        public ICommand ApprovePickedSongs { get; }
        public ICommand Initialize { get; }
        public event Action SongsAdded;
        public AddSongsViewModel(PlaylistViewModel playlistView,
            //IMusicManagementQueryService queryService,
            //IPlaylistService playlistService,
            IPopupNavigation navigation)
        {
            _playlistView = playlistView;
            //_queryService = queryService;
            //_playlistService = playlistService;
            _navigation = navigation;
            ApprovePickedSongs = new Command(async () => await OnApprovePickedSongs());
            Initialize = new Command(async () => await OnInitialize());
        }

        private async Task OnInitialize()
        {
            //var songs = await _queryService.GetAllMusicNotOnPlaylist(_playlistView.Id);
            //Device.BeginInvokeOnMainThread(
            //() =>
            //{
            //    songs.ToList().ForEach(s => Songs.Add(new SongViewModel(s.SongId, s.Title, s.ImageSource)));
            //});
        }


        private async Task OnApprovePickedSongs()
        {
            //await _playlistService.AddSongsToExisting(_playlistView.Id, Songs.Where(x => x.IsPicked).Select(x => x.Id));
            await _navigation.PopAsync(true);
            SongsAdded?.Invoke();
        }
    }
}