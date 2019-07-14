using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.Droid.MusicDownloading;
using YoutubeMusicPlayer.MusicDownloading;
using Stream = System.IO.Stream;

[assembly:Dependency(typeof(Downloader))]
namespace YoutubeMusicPlayer.Droid.MusicDownloading
{
    public class Downloader:IDownloader
    {
        public async Task<Stream> GetStreamAsync(string url, Action<int> onProgress)
        {
            var webClient = new WebClient();

            webClient.DownloadProgressChanged += (o, ea) =>
            {
                var contentLength = double.Parse(webClient.ResponseHeaders[HttpRequestHeader.ContentLength]);
                onProgress?.Invoke((int)((ea.BytesReceived / contentLength) * 100));
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