using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using YoutubeMusicPlayer.Domain.MusicDownloading;

namespace YoutubeMusicPlayer.MusicDownloading
{
    public class YtMp3DownloadLinkGenerator:IDownloadLinkGenerator
    {
        private readonly IScriptIdEncoder _encoder;

        private readonly Dictionary<int, string> _servers=new Dictionary<int, string>
        {
            { 1, "cco" },
            { 2, "aea" },
            { 3, "oea" },
            { 4, "aoa" },
            { 5, "cee" },
            { 6, "coe" },
            { 7, "oca" },
            { 8, "caa" },
            { 9, "eae" },
            { 10, "oce" },
            { 11, "eao" },
            { 12, "oco" },
            { 13, "eoo" },
            { 14, "coc" },
            { 15, "aco" },
            { 16, "aae" },
            { 17, "coo" },
            { 18, "ooa" },
            { 19, "cao" },
            { 20, "aoe" },
            { 21, "oeo" },
            { 22, "ece" },
            { 23, "eeo" },
            { 24, "oac" },
            { 25, "eec" },
            { 26, "oec" },
            { 27, "eoe" },
            { 28, "eaa" },
            { 29, "eoa" },
            { 30, "ecc" },
            { 31, "cec" },
            { 32, "ceo" },
            { 33, "aee" },
            { 34, "cae" },
            { 35, "eoc" },
            { 36, "oae" },
            { 37, "cce" },
            { 38, "ooe" },
            { 39, "aao" },
            { 40, "aec" },
            { 41, "cca" },
            { 42, "oaa" }
        };

        private bool _serverNotFoundYet = false;

        public YtMp3DownloadLinkGenerator(IScriptIdEncoder encoder)
        { 
            _encoder = encoder;
        }

        public async Task<string> GenerateLinkAsync(string musicIdFromYoutube)
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

            var hash = info.Item2;
            if (_serverNotFoundYet)
            {
                hash = await Progress(hash: info.Item2);
            }

            var downloadUrl = $"https://{_servers[info.Item1]}.oeaa.cc/{hash}/{musicIdFromYoutube}";

            return downloadUrl;
        }

        private async Task<string> Progress(string hash)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Referer", "https://ytmp3.cc/");
            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.1.5");
            var url = $"https://a.oeaa.cc/progress.php?callback=jQuery33109514798167395044_1532263442793&id={hash}&_1532263442795";
            var maxTime = TimeSpan.FromSeconds(60);
            var startTime = DateTime.Now;
            while (true && DateTime.Now - startTime <= maxTime)
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                var beg = content.IndexOf('{');

                var end = content.LastIndexOf('}');

                if (beg == -1 || end == -1)
                {
                    //Means server couldn't handle request as we expected.
                    throw new Exception($"ytmp3.cc server couldn\'t handle our request.\nReturned error:{content}");
                }

                var json = content.Substring(beg, end - beg + 1);
                var info = JsonConvert.DeserializeAnonymousType(json, new { sid = "", progress = "" });

                if (info.progress == "3")
                    return info.sid;
                await Task.Delay(3000);
            }
            throw new Exception("Exceeded limit of time for download.");
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string musicIdFromYoutube)
        {
            var scriptId = await GetScriptId();

            scriptId = await _encoder.EncodeAsync(scriptId);

            var url = $"https://a.oeaa.cc/check.php?callback=jQuery3310147368603350448_1558729784771&v={musicIdFromYoutube}&f=mp3&k={scriptId}&_=1558729784773";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Referer", "https://ytmp3.cc/");
            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.1.5");


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

            var srcValue = doc.DocumentNode.Descendants("script").ElementAt(1)
                ?.Attributes["src"].Value;
                       
            if(srcValue==null)
                throw new Exception($"Error occured while getting scriptId");

            return Regex.Match(input: srcValue, pattern: @"[a-z]{1}\=[a-zA-Z0-9\-\\_]{16,40}").ToString().Substring(2);
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

            if (sid == 0)
                _serverNotFoundYet = true;

            return new Tuple<int, string>(sid != 0 ? sid : 1, info.hash);                           
        }
    }
}
