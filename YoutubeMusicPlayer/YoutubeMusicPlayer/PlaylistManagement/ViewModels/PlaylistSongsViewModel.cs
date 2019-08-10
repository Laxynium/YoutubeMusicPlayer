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
    public class PlaylistSongsViewModel : ViewModelBase
    {
        private readonly PlaylistViewModel _parentViewModel;
        private readonly IPopupNavigation _popupNavigation;
        private readonly IPlaylistService _playlistService;
        private readonly IAddSongsViewModelFactor _addSongsFactor;
        private readonly IMusicManagementQueryService _queryService;
        public ObservableCollection<SongViewModel> Songs { get; } = new ObservableCollection<SongViewModel>();

        public ICommand GoBack { get; }
        public ICommand Initialize { get; }
        public ICommand AddSongs { get; }
        public ICommand RemoveSongs { get; }
        public ICommand ChangeState { get; }

        private bool _isNormalState = true;
        public bool IsNormalState
        {
            get =>_isNormalState;
            set => SetValue(ref _isNormalState,value);
        }

        public PlaylistSongsViewModel(
            PlaylistViewModel parentViewModel, 
            IPopupNavigation popupNavigation, 
            IPlaylistService playlistService, 
            IAddSongsViewModelFactor addSongsFactor,
            IMusicManagementQueryService queryService
        )
        {
            _parentViewModel = parentViewModel;
            _popupNavigation = popupNavigation;
            _playlistService = playlistService;
            _addSongsFactor = addSongsFactor;
            _queryService = queryService;
            GoBack = new Command(
                async () => await _popupNavigation.PopAsync(true)
            );
            Initialize = new Command(
                async () => await OnInitialize()
            );
            AddSongs = new Command(
                async () => { await OnAddSongs(); }
            );
            RemoveSongs = new Command(async()=>await OnRemoveSongs());
            ChangeState = new Command(OnChangeState);
        }

        private void OnChangeState()
        {
            IsNormalState = !IsNormalState;
        }


        private async Task OnRemoveSongs()
        {
            var songsToRemove = Songs.Where(x => x.IsPicked).ToList();
            await _playlistService.RemoveSongsFromExisting(_parentViewModel.Id, songsToRemove.Select(x => x.Id));
            IsNormalState = !IsNormalState;
            songsToRemove.ForEach(x=>Songs.Remove(x));
        }

        private async Task OnAddSongs()
        {
            var vM = _addSongsFactor.Create(_parentViewModel);
            vM.SongsAdded += async () => await OnInitialize();
            await _popupNavigation.PushAsync(new AddSongsPopupPage(vM), true);
        }

        private async Task OnInitialize()
        {
            var songs = await _queryService.GetAllSongsFromPlaylist(_parentViewModel.Id);
            foreach (var x in songs)
                if (Songs.FirstOrDefault(y => y.Id == x.SongId) is null)
                    Songs.Add(new SongViewModel(x.SongId, x.Title, x.ImageSource));
        }
    }
}