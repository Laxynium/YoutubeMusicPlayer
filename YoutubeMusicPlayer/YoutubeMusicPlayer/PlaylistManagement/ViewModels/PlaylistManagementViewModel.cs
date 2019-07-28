using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Domain.MusicManagement.Queries;
using YoutubeMusicPlayer.Framework;

namespace YoutubeMusicPlayer.PlaylistManagement.ViewModels
{
    public class PlaylistManagementViewModel : ViewModelBase
    {
        private readonly IPopupNavigation _popupNavigation;
        private readonly ISelectSongsViewModelFactory _viewModelFactory;
        private readonly IPlaylistService _playlistService;
        private readonly IMusicManagementQueryService _queryService;
        public ICommand Initialize { get; }
        public ICommand CreatePlaylist { get; }
        public ICommand RemovePlaylist { get; }
        public ICommand AddSongToPlaylist { get; }
        public ICommand RemoveSongFromPlaylist { get; }
        public ICommand RenamePlaylist { get; }

        private ObservableCollection<PlaylistViewModel> _playlists = new ObservableCollection<PlaylistViewModel>();

        public ObservableCollection<PlaylistViewModel> Playlists
        {
            get => _playlists;
            set => SetValue(ref _playlists, value);
        }

        public PlaylistManagementViewModel(
            IPopupNavigation popupNavigation, 
            ISelectSongsViewModelFactory viewModelFactory,
            IPlaylistService playlistService,
            IMusicManagementQueryService queryService)
        {
            _popupNavigation = popupNavigation;
            _viewModelFactory = viewModelFactory;
            _playlistService = playlistService;
            _queryService = queryService;
            Initialize = new Command(async()=> await UpdatePlaylists());
            CreatePlaylist = new Command(async () => await OnCreatePlaylist());
            RemovePlaylist = new Command<PlaylistViewModel>(async (vM) => await OnRemovePlaylist(vM));
            AddSongToPlaylist = new Command<PlaylistViewModel>(async(vM)=>await OnAddSongToPlaylist(vM));
            RemoveSongFromPlaylist = new Command<PlaylistViewModel>(async(vM)=>await OnRemoveSongFromPlaylist(vM));
            RenamePlaylist = new Command<PlaylistViewModel>(async(vM)=>await OnRenamePlaylist(vM));
        }


        private PlaylistViewModel _playlistViewModel;

        private async Task OnRenamePlaylist(PlaylistViewModel playlist)
        {
            _playlistViewModel = playlist;
            var vM = new RenamePlaylistViewModel(playlist.Name);
            vM.OnSubmit += OnRenameSubmit;
            await _popupNavigation.PushAsync(new RenamePlaylistPopup(vM));
        }

        private async void OnRenameSubmit(object sender, string newName)
        {
            await _popupNavigation.PopAsync();
            await _playlistService.UpdateName(_playlistViewModel.Id, newName);
            _playlistViewModel.Name = newName;
        }

        private async Task OnRemoveSongFromPlaylist(PlaylistViewModel playlist)
        {
            _playlistViewModel = playlist;
            var songSelectViewModel = _viewModelFactory.Create<DeleteSongsViewModel>(playlist);
            songSelectViewModel.OnApprovedDeletedSongs += OnApprovedDeletedSongs;
            await _popupNavigation.PushAsync(new DeleteSongsPopup(songSelectViewModel));
            
        }

        private async void OnApprovedDeletedSongs(object sender, IEnumerable<SongViewModel> e)
        {
            await _popupNavigation.PopAsync(true);
            await _playlistService.RemoveSongsFromExisting(_playlistViewModel.Id,e.Select(x=>x.Id));
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

        private async Task OnCreatePlaylist()
        {
            var vM  = new CreatePlaylistViewModel();
            vM.OnSubmit += OnSubmit;
            await _popupNavigation.PushAsync(new CreatePlaylistPopup(vM));
        }

        private async void OnSubmit(object sender, string name)
        {
            await _popupNavigation.PopAsync(true);
            await _playlistService.Create(name);
            await UpdatePlaylists();
        }

        private async Task OnAddSongToPlaylist(PlaylistViewModel playlist)
        {
            _playlistViewModel = playlist;
            var songSelectViewModel = _viewModelFactory.Create<SelectSongsViewModel>(playlist);
            songSelectViewModel.OnApprovedPickedSongs += OnApprovedPickedSongs;
            await _popupNavigation.PushAsync(new SelectSongsPopup(songSelectViewModel)); //TODO think about what should be created by factory vM or PopupPage
        }

        private async void OnApprovedPickedSongs(object sender, IEnumerable<SongViewModel> approvedSongs)
        {
            await _popupNavigation.PopAsync(true);
            await _playlistService.AddSongsToExisting(_playlistViewModel.Id, approvedSongs.Select(x=>x.Id));
        }
    }
}