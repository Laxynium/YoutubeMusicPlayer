using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicManagement
{
    public static class MusicManagementStartup
    {
        public static void Initialize(Container container, string connectionString)
        {
            container.Register(
                () =>
                {
                    var builder = new DbContextOptionsBuilder();
                    builder.UseSqlite(connectionString);
                    var dbContext = new MusicManagementDbContext(builder.Options);
                    return dbContext;
                }, Lifestyle.Scoped);

            container.Register<DbContext>(container.GetInstance<MusicManagementDbContext>, Lifestyle.Scoped);
            container.Register<PlaylistRepository>(Lifestyle.Scoped);
            container.Register(typeof(ICommandHandler<>), typeof(MusicManagementStartup).Assembly, Lifestyle.Scoped);
        }
    }
}