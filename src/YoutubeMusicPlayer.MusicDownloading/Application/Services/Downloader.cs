using System;
using System.Net;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Services
{
    internal class Downloader
    {
        internal async Task<Result<byte[]>> GetStreamAsync(string url, Action<int> onProgress)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("user-agent", "PostmanRuntime/7.15.0'");

            webClient.DownloadProgressChanged += (o, ea) =>
            {
                try
                {
                    if (!double.TryParse(webClient.ResponseHeaders[HttpRequestHeader.ContentLength],
                        out var contentLength)) return;
                    onProgress?.Invoke((int) ((ea.BytesReceived / contentLength) * 100));
                }
                catch (Exception e)
                {
                    //skip
                }
                
            };

            async Task<byte[]> Action() => await webClient.DownloadDataTaskAsync(new Uri(url));
            string ErrorHandler(Exception e) => e.Message;

            return await Result.Try(Action, ErrorHandler);
        }

    }
}