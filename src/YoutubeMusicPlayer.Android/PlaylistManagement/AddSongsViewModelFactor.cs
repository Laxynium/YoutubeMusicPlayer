//using Ninject;
//using YoutubeMusicPlayer.Domain.MusicManagement;
//using YoutubeMusicPlayer.Domain.MusicManagement.Queries;
//using YoutubeMusicPlayer.PlaylistManagement.Factories;
//using YoutubeMusicPlayer.PlaylistManagement.ViewModels;
//using IPopupNavigation = YoutubeMusicPlayer.Framework.IPopupNavigation;

//namespace YoutubeMusicPlayer.Droid.PlaylistManagement
//{
//    internal class AddSongsViewModelFactor : IAddSongsViewModelFactor
//    {
//        private readonly IKernel _kernel;

//        public AddSongsViewModelFactor(IKernel kernel)
//        {
//            _kernel = kernel;
//        }
//        public AddSongsViewModel Create(PlaylistViewModel parent) => 
//            new AddSongsViewModel
//            (
//                parent,
//                _kernel.Get<IMusicManagementQueryService>(),
//                _kernel.Get<IPlaylistService>(),
//                _kernel.Get<IPopupNavigation>()
//            );
//    }
//}