using System;
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
        public Downloader()
        {
            
        }
        public async Task<Stream> GetStreamAsync(string url, Action<int> onProgress)
        {
            int receivedBytes = 0;
            int totalBytes = 0;
            WebClient client = new WebClient();

            using (var stream = await client.OpenReadTaskAsync(url))
            {
                byte[] buffer = new byte[4096];
                totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);

                using (var memoryStream = new MemoryStream())
                {
                    for (; ; )
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        await memoryStream.WriteAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            await Task.Yield();
                            break;
                        }

                        receivedBytes += bytesRead;
                        double percentage = (double)receivedBytes / totalBytes * 100D;
                        onProgress?.Invoke((int)percentage);
                    }
                    return new MemoryStream(memoryStream.ToArray());
                }

            }

        }
    }
}