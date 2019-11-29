using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.MusicDownloading.Services
{
    internal class Downloader
    {
        internal async Task<Result<byte[], (string msg, Exception e)>> GetStreamAsync(string url, Action<int> onProgress)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("user-agent", "PostmanRuntime/7.15.0'");

            webClient.DownloadProgressChanged += (o, ea) =>
            {
                try
                {
                    var contentLength = double.Parse(webClient.ResponseHeaders[HttpRequestHeader.ContentLength]);
                    onProgress?.Invoke((int) ((ea.BytesReceived / contentLength) * 100));
                }
                catch (Exception e)
                {

                }
               
            };
            try
            {
                var result = await webClient.DownloadDataTaskAsync(new Uri(url));

                return Result.Ok<byte[], (string msg, Exception)>(result);
            }
            catch (Exception e)
            {
                return Result.Failure<byte[], (string msg, Exception)>((e.Message, e));
            }

        }
    }
}