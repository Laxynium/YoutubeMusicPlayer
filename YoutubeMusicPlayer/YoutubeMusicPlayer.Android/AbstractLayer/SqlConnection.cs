using System;
using System.IO;
using SQLite;
using YoutubeMusicPlayer.AbstractLayer;
using Xamarin.Forms;
using YoutubeMusicPlayer.Droid.AbstractLayer;

[assembly:Dependency(typeof(SqlConnection))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class SqlConnection:ISqlConnection
    {
        public SQLiteAsyncConnection GetConnection()
        {
           var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

           var path = Path.Combine(documentsPath, "musicDb.db3");

           return new SQLiteAsyncConnection(path);
        }
    }
}