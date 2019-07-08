using Xamarin.Forms;
using YoutubeMusicPlayer.Framework;

[assembly: Dependency(typeof(ISqlConnection))]
namespace YoutubeMusicPlayer.Framework
{
    public interface ISqlConnection
    {
        SQLite.SQLiteAsyncConnection GetConnection();
    }
}
