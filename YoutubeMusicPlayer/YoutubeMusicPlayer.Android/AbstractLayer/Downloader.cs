﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using YoutubeMusicPlayer.AbstractLayer;
using Xamarin.Forms;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using YoutubeMusicPlayer.Models;

[assembly:Dependency(typeof(Downloader))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class Downloader:IDownloader
    {
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
                return null;
            }                 
        }
    }
}