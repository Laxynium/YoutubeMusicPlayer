using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;

[assembly: Dependency(typeof(ISqlConnection))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface ISqlConnection
    {
        SQLite.SQLiteAsyncConnection GetConnection();
    }
}
