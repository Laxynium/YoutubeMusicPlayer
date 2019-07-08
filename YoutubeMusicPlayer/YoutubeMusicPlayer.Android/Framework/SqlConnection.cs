using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Droid.Framework;
using YoutubeMusicPlayer.Framework;

[assembly:Dependency(typeof(SqlConnection))]
namespace YoutubeMusicPlayer.Droid.Framework
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