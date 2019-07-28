using Ninject;
using Ninject.Parameters;
using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.Framework
{
    public class SelectSongsViewModelFactory : ISelectSongsViewModelFactory
    {
        private readonly IKernel _kernel;

        public SelectSongsViewModelFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

       

        //public T Create<T>(PlaylistViewModel playlistViewModel) where T: ViewModelBase
        //{
        //    return _kernel.Get<SelectSongsViewModel>(new ConstructorArgument("playlistView", playlistViewModel));
        //}

        public T Create<T>(PlaylistViewModel playlistViewModel) where T : ViewModelBase
        {
            return _kernel.Get<T>(new ConstructorArgument("playlistView", playlistViewModel));
        }
    }
}