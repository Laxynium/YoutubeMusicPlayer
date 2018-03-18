using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public class YtMp3DownloadService:IDownloadService
    {
        private readonly IDownloader _downloader;

        private readonly Dictionary<int, string> _servers=new Dictionary<int, string>
        {
            {1,"odg"},
            {2,"ado"},
            {3,"jld"},
            {4,"tzg"},
            {5,"uuj"},
            {6,"bkl"},
            {7,"fnw"},
            {8,"eeq"},
            {9,"ebr"},
            {10,"asx"},
            {11,"ghn"},
            {12,"eal"},
            {13,"hrh"},
            {14,"quq"},
            {15,"zki"},
            {16,"tff"},
            {17,"aol"},
            {18,"eeu"},
            {19,"kkr"},
            {20,"yui"},
            {21,"yyd"},
            {22,"hdi"},
            {23,"ddb"},
            {24,"iir"},
            {25,"ihi"},
            {26,"heh"},
            {27,"xaa"},
            {28,"nim"},
            {29,"omp"},
            {30,"eez"},
            {31,"rpx"},
            {32,"cxq"},
            {33,"typ"},
            {34,"amv"},
            {35,"rlv"},
            {36,"xnx"},
            {37,"vro"},
            {38,"pfg"}
        };

        public YtMp3DownloadService(IDownloader downloader)
        {
            _downloader = downloader;
        }

        public event EventHandler<int> OnProgressChanged;

        public async Task<Stream> DownloadMusicAsync(string musicIdFromYoutube,INotifyProgressChanged onProgressChanged)
        {
            var response = await GetResponseAsync(musicIdFromYoutube);


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Request failed : {response}");
            }
     
            var info = await ParseResponseAsync(response);

            //if new server is added
            if (!_servers.ContainsKey(info.Item1))
            {
                throw new Exception($"Could not find server with id '{info.Item1}' in dictionary.");
            }

            var downloadUrl = $"https://{_servers[info.Item1]}.ymcdn.cc/{info.Item2}/{musicIdFromYoutube}";

            return await DownloadAsync(downloadUrl, onProgressChanged);
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string musicIdFromYoutube)
        {
            var scriptId = await GetScriptId();

            var url = $"https://d.ymcdn.cc/check.php?callback=https://d.ymcdn.cc/check.php?callback=jQuery33106493845259851172_1518788953932&v={musicIdFromYoutube}&f=mp3&k={scriptId}&_=2518788953934";
                  
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Host", "d.ymcdn.cc");
            client.DefaultRequestHeaders.Add("Referer", "https://ytmp3.cc/");
            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.1.1");

            var response = await client.GetAsync(url);

            return response;
        }

        private async Task<string> GetScriptId()
        {
            var client = new HttpClient();
            var response=await client.GetAsync("https://ytmp3.cc/");
            var responseAsString=await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(responseAsString);
            var tmp = doc.DocumentNode.Descendants("script")
                .Where(e => e.Attributes["src"].Value.Contains("js/converter")).Take(1);
            var srcValue = doc.DocumentNode.Descendants("script").ElementAt(1)
                ?.Attributes["src"].Value;
                       
            if(srcValue==null)
                throw new Exception($"Error occured while getting scriptId");

            var scriptId=Regex.Match(input:srcValue,pattern:@"[a-z]{1}\=[a-zA-Z0-9\-\\_]{4,32}").ToString().Substring(2);

            return scriptId;
        }

        private async Task<Tuple<int, string>> ParseResponseAsync(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();

            var beg = result.IndexOf('{');

            var end = result.LastIndexOf('}');

            if (beg == -1 || end == -1)
            {
                //Means server couldn't handle request as we expected.
                throw new Exception($"ytmp3.cc server couldn\'t handle our request.\nReturned error:{result}");
            }
         
            var json = result.Substring(beg, end - beg + 1);
            var info = JsonConvert.DeserializeAnonymousType(json, new { sid = "", hash = "" });

            var sid = Convert.ToInt32(info.sid);

            return new Tuple<int, string>(sid != 0 ? sid : 1, info.hash);                           
        }

        private async Task<Stream> DownloadAsync(string downloadUrl,INotifyProgressChanged onProgressChanged)
        {
            var result = await _downloader.GetStreamAsync(downloadUrl, onProgressChanged);
            
            return result;
        }
    }
}
