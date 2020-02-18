using System.Linq;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using YoutubeMusicPlayer.Framework;
using YoutubeMusicPlayer.Framework.Messaging;
using YoutubeMusicPlayer.MusicBrowsing;
using YoutubeMusicPlayer.MusicDownloading;
using YoutubeMusicPlayer.MusicManagement;

namespace YoutubeMusicPlayer.Application
{
    public class YoutubeMusicPlayerOptions
    {
        public string DatabasePath { get; set; }
        public string MusicDirectoryPath { get; set; }
    }
    public static class YoutubeMusicPlayerStartup
    {
        public static Container SetupApplication(
            this Container container, 
            YoutubeMusicPlayerOptions options,
            Assembly[] eventHandlersAssemblies = null) //TODO find a way append registrations to given collection
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;

            SetupModules(container,options);

            var assemblies = new[]{
                typeof(MusicManagementStartup).Assembly,
                typeof(MusicBrowsingStartup).Assembly
            };

            container.Collection.Register(typeof(IEventHandler<>), assemblies.Union(eventHandlersAssemblies ?? new Assembly[]{}));

            return container;
        }
        private static void SetupModules(Container container, YoutubeMusicPlayerOptions options)
        {
            var connectionString = $"Data Source={options.DatabasePath};";
            FrameworkStartup.Initialize(container, connectionString);
            MusicManagementStartup.Initialize(container, connectionString, options.MusicDirectoryPath);
            MusicBrowsingStartup.Initialize(container);
        }
    }
}