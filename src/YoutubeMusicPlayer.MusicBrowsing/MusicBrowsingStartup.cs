using SimpleInjector;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicBrowsing
{
    public static class MusicBrowsingStartup
    {
        public static void Initialize(Container container)
        {
            container.Register(typeof(IQueryHandler<,>), typeof(MusicBrowsingStartup).Assembly, Lifestyle.Singleton);
        }
    }
}