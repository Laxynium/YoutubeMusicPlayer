//using Ninject;
//using YoutubeMusicPlayer.Domain.MusicManagement;
//using YoutubeMusicPlayer.Domain.MusicManagement.Queries;
//using YoutubeMusicPlayer.Framework;
//using YoutubeMusicPlayer.PlaylistManagement.Factories;
//using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

//namespace YoutubeMusicPlayer.Droid.PlaylistManagement
//{
//    internal class PlaylistSongsViewModelFactor : IPlaylistSongsViewModelFactor
//    {
//        private readonly IKernel _kernel;

//        public PlaylistSongsViewModelFactor(IKernel kernel)
//        {
//            _kernel = kernel;
//        }

//        public PlaylistSongsViewModel Create(PlaylistViewModel parent)
//        {
//            return new PlaylistSongsViewModel(
//                parent,
//                _kernel.Get<IPopupNavigation>(),
//                _kernel.Get<IPlaylistService>(),
//                _kernel.Get<IAddSongsViewModelFactor>(),
//                _kernel.Get<IMusicManagementQueryService>()
//            );
//        }
//    }
//}