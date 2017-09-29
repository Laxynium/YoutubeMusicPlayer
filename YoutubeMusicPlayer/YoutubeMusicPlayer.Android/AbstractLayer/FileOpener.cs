using System.Threading.Tasks;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using Uri = Android.Net.Uri;


[assembly:Dependency(typeof(FileOpener))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class FileOpener:IFileOpener
    {
        public async Task OpenFile(string filePath)
        {
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(Uri.FromFile(new File(filePath)),"audio/*");

            Forms.Context.StartActivity(intent);
        }
    }
}