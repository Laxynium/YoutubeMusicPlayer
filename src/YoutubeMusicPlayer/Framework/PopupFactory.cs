using YoutubeMusicPlayer.PlaylistManagement.ViewModels;

namespace YoutubeMusicPlayer.Framework
{
    public class SelectSongsViewModelFactory : ISelectSongsViewModelFactory
    {
        public SelectSongsViewModelFactory()
        {
        }

       

        //public T Create<T>(PlaylistViewModel playlistViewModel) where T: ViewModelBase
        //{
        //    return _kernel.Get<SelectSongsViewModel>(new ConstructorArgument("playlistView", playlistViewModel));
        //}

        public T Create<T>(PlaylistViewModel playlistViewModel) where T : ViewModelBase,new()
        {
            return new T();
            //return _kernel.Get<T>(new ConstructorArgument("playlistView", playlistViewModel));
        }
    }
}