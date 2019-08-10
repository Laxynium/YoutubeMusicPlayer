using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Domain.MusicManagement.Queries;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.PlaylistManagement.Factories;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class PlaylistManagementViewModel : ViewModelBase
    {
        private readonly IPopupNavigation _popupNavigation;
        private readonly IPlaylistService _playlistService;
        private readonly IPlaylistSongsViewModelFactor _playlistSongsFactor;
        private readonly IMusicManagementQueryService _queryService;
        public ICommand Initialize { get; }
        public ICommand StartCreatingPlaylist { get; }
        public ICommand CancelCreatingPlaylist { get; }
        public ICommand CreatePlaylist { get; }
        public ICommand RemovePlaylist { get; }
        public ICommand RenamePlaylist { get; }
        public ICommand OpenAddSongs { get; }
        private ObservableCollection<PlaylistViewModel> _playlists = new ObservableCollection<PlaylistViewModel>();

        public ObservableCollection<PlaylistViewModel> Playlists
        {
            get => _playlists;
            set => SetValue(ref _playlists, value);
        }

        private bool _isAddPlaylistState;

        public bool IsAddPlaylistState
        {
            get => _isAddPlaylistState;
            set => SetValue(ref _isAddPlaylistState, value);
        }

        private string _newPlaylistName;
        

        public string NewPlaylistName
        {
            get => _newPlaylistName;
            set => SetValue(ref _newPlaylistName,value);
        }

        private PlaylistViewModel _selectedPlaylist;

        public PlaylistViewModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set => SetValue(ref _selectedPlaylist,value);
        }

        public PlaylistManagementViewModel(
            IPopupNavigation popupNavigation,
            IPlaylistService playlistService,
            IPlaylistSongsViewModelFactor playlistSongsFactor,
            IMusicManagementQueryService queryService)
        {
            _popupNavigation = popupNavigation;
            _playlistService = playlistService;
            _playlistSongsFactor = playlistSongsFactor;
            _queryService = queryService;
            Initialize = new Command(async()=> await UpdatePlaylists());
            StartCreatingPlaylist = new Command( OnStartCreatingPlaylist);
            CancelCreatingPlaylist = new Command(OnCancelCreatingPlaylist);
            CreatePlaylist = new Command(async()=>await OnCreatePlaylist());
            RemovePlaylist = new Command<PlaylistViewModel>(async (vM) => await OnRemovePlaylist(vM));
            RenamePlaylist = new Command<PlaylistViewModel>(async(vM)=>await OnRenamePlaylist(vM));
            OpenAddSongs = new Command<PlaylistViewModel>(async(vM)=>await OnOpenAddSongs(vM));
        }

        private async Task OnOpenAddSongs(PlaylistViewModel vM)
        {
            if (vM is null)
                return;
            SelectedPlaylist = null;

            await _popupNavigation.PushAsync(new PlaylistSongsPopupPage(_playlistSongsFactor.Create(vM)));
        }

        private void OnStartCreatingPlaylist()
        {
            IsAddPlaylistState = true;
        }

        private void OnCancelCreatingPlaylist()
        {
            IsAddPlaylistState = false;
        }

        private async Task OnCreatePlaylist()
        {
            if(string.IsNullOrWhiteSpace(NewPlaylistName))
                return;
            await _playlistService.Create(NewPlaylistName);
            IsAddPlaylistState = false;
            await UpdatePlaylists();
        }

        private async Task OnRenamePlaylist(PlaylistViewModel playlist)
        {
            await _playlistService.UpdateName(playlist.Id, playlist.NewName);
            playlist.UpdateName();
        }


        private async Task OnRemovePlaylist(PlaylistViewModel playlist)
        {
            await _playlistService.Remove(playlist.Id);
            Playlists.Remove(playlist);
        }

        private async Task UpdatePlaylists()
        {
            (await _queryService.GetAllPlaylists())
                .Select(x => new PlaylistViewModel(x.Id,x.Name))
                .ToList()
                .ForEach(
                    x =>
                    {
                        if (_playlists.SingleOrDefault(y => y.Id == x.Id) is null)
                            _playlists.Add(x);
                    });
        }

       
    
    }
}