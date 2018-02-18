using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using YoutubeMusicPlayer.AbstractLayer;
using Xamarin.Forms;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using YoutubeMusicPlayer.Models;
using Stream = System.IO.Stream;

[assembly:Dependency(typeof(Downloader))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class Downloader:IDownloader
    {
        public Downloader()
        {
            
        }
        public async Task<Stream> GetStreamAsync(string url,INotifyProgressChanged onProgressChanged)
        {
           var webClient=new WebClient();

           webClient.DownloadProgressChanged += (o, ea) =>
           {
               onProgressChanged.OnProgressChanged(ea.ProgressPercentage);
           };
            try
            {
                var result = await webClient.DownloadDataTaskAsync(new Uri(url));

                return new MemoryStream(result);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Something has gone wrong in request{url}");
                throw new Exception("Some error occured while downloading music.", e);
            }                 
        }
    }
}