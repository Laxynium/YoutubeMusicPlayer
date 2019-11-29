using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace YoutubeMusicPlayer.MusicDownloading.Services
{
    internal class YtMp3DownloadLinkGenerator
    {
        private readonly ScriptIdEncoder _encoder;

        private readonly Dictionary<int, string> _servers=new Dictionary<int, string>
        {
            {1, "fff"},
            {2, "ffr"},
            {3, "fij"},
            {4, "flr"},
            {5, "frr"},
            {6, "fti"},
            {7, "ftl"},
            {8, "ift"},
            {9, "iil"},
            {10, "iir"},
            {11, "ijl"},
            {12, "ijt"},
            {13, "ilj"},
            {14, "irf"},
            {15, "itj"},
            {16, "jif"},
            {17, "jjf"},
            {18, "jjj"},
            {19, "jli"},
            {20, "jrt"},
            {21, "jti"},
            {22, "lii"},
            {23, "lit"},
            {24, "ljf"},
            {25, "ljt"},
            {26, "llj"},
            {27, "lrf"},
            {28, "ltl"},
            {29, "rff"},
            {30, "rft"},
            {31, "rif"},
            {32, "rjj"},
            {33, "rjl"},
            {34, "rri"},
            {35, "rtf"},
            {36, "rtr"},
            {37, "rtt"},
            {38, "tfj"},
            {39, "tft"},
            {40, "tjj"},
            {41, "tlf"},
            {42, "ttj"}
        };

        private bool _serverNotFoundYet = false;

        public YtMp3DownloadLinkGenerator()
        { 
            _encoder = new ScriptIdEncoder();
        }

        internal async Task<string> GenerateLinkAsync(string musicIdFromYoutube)
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

            var downloadUrl = $"https://{_servers[info.Item1]}.fjrifj.frl/{hash}/{musicIdFromYoutube}";

            return downloadUrl;
        }

        private async Task<string> Progress(string hash)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Referer", "https://ytmp3.cc/");
            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.1.5");
            var url = $"https://i.fjrifj.frl/progress.php?callback=jQuery33109514798167395044_1532263442793&id={hash}&_1532263442795";
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

            var url = $"https://i.fjrifj.frl/check.php?callback=jQuery3310147368603350448_1558729784771&v={musicIdFromYoutube}&f=mp3&k={scriptId}&_=1558729784773";

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
