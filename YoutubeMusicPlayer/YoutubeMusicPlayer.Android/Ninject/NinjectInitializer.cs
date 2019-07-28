using Ninject;
using YoutubeMusicPlayer.Persistence;

namespace YoutubeMusicPlayer.Droid.Ninject
{
    public static class NinjectInitializer
    {
        public static IKernel Kernel { get; private set; }
        public static void Initialize()
        {
            var settings = new NinjectSettings{LoadExtensions = false};
            Kernel = new StandardKernel(settings, new YoutubeMusicPlayerModule(), new DatabaseModule());
        }
    }
}