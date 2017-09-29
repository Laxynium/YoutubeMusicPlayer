﻿using System;
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
        public async Task<Stream> GetStream(string url,IOnProgressChanged onProgressChanged)
        {
           var webClient=new WebClient();

            onProgressChanged.ProgressChanged += (o, v) =>
            {
                onProgressChanged.Value = v / 100f;
            };

           webClient.DownloadProgressChanged += (o, ea) =>
           {
               onProgressChanged.OnProgressChanged(ea.ProgressPercentage);
           };

           var result=await webClient.DownloadDataTaskAsync(new Uri(url));

            return new MemoryStream(result);
            
        }
    }
}