using Ninject;

namespace YoutubeMusicPlayer.Droid.Ninject
{
    public static class NinjectInitializer
    {
        public static IKernel Kernel { get; private set; }
        public static void Initialize()
        {
            Kernel = new StandardKernel(new YoutubeMusicPlayerModule());
        }
    }
}