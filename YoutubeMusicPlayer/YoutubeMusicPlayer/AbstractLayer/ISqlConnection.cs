using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;

[assembly:Dependency(typeof(ISqlConnection))]
namespace YoutubeMusicPlayer.AbstractLayer
{
    public interface ISqlConnection
    {
        SQLite.SQLiteAsyncConnection GetConnection();
    }
}
