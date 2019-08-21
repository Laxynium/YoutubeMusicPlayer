using Ninject.Modules;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Domain.MusicManagement;
using YoutubeMusicPlayer.Domain.MusicManagement.Queries;
using YoutubeMusicPlayer.Domain.MusicPlaying;
using YoutubeMusicPlayer.Domain.MusicPlayingNew;
using YoutubeMusicPlayer.Persistence.Queries;
using YoutubeMusicPlayer.Persistence.Repositories;

namespace YoutubeMusicPlayer.Persistence
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISongRepository>().To<SongRepository>().InSingletonScope();
            Bind<IPlaylistRepository>().To<PlaylistRepository>().InSingletonScope();
            Bind<IMusicManagementQueryService>().To<MusicManagementQueryService>().InSingletonScope();
        }
    }
}