using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.MusicManagement.Infrastructure.Services.Youtube
{
    internal sealed class Downloader
    {
        internal async Task<Result<byte[]>> GetStreamAsync(string url, Action<int> onProgress)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("user-agent", "PostmanRuntime/7.15.0'");

            //var obs = Observable.Create<string>(
            //    (IObserver<string> observer) =>
            //    {
            //        observer.OnNext("objectX created");

            //        Observable.FromEventPattern<DownloadProgressChangedEventHandler, DownloadProgressChangedEventArgs>(
            //            h => webClient.DownloadProgressChanged += h,
            //            h => webClient.DownloadProgressChanged -= h
            //        )
            //            .Select(x=>x.EventArgs)
            //            .Subscribe(x=>observer.OnNext(x.BytesReceived.ToString()));

            //        Observable.FromEventPattern<DownloadDataCompletedEventHandler, DownloadDataCompletedEventArgs>(
            //                h => webClient.DownloadDataCompleted += h,
            //                h => webClient.DownloadDataCompleted -= h
            //            )
            //            .Select(x => x.EventArgs)
            //            .Subscribe(x => observer.OnNext("finished"));

            //        return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
            //    });

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