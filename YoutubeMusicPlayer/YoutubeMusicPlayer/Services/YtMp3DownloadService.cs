using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public class YtMp3DownloadService:IDownloadService
    {
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
        };

        public event EventHandler<int> OnProgressChanged;

        public async Task<Stream> DownloadMusicAsync(string musicIdFromYoutube,INotifyProgressChanged onProgressChanged)
        {
            var response = await GetResponseAsync(musicIdFromYoutube);

            if (!response.IsSuccessStatusCode)
                return null;

            var info = await ParseResponseAsync(response);


            var downloadUrl = $"https://{_servers[info.Item1]}.ymcdn.cc/{info.Item2}/{musicIdFromYoutube}";

            return await DownloadAsync(downloadUrl, onProgressChanged);
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string musicIdFromYoutube)
        {
            var url = 
                $"https://d.ymcdn.cc/check.php?callback=jQuery321018152062429700822_1506173145863&v={musicIdFromYoutube}&f=mp3&_=1506173145865";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Referer", "https://ytmp3.cc/");
            client.DefaultRequestHeaders.Add("Host", "d.ymcdn.cc");
            try
            {
                var response = await client.GetAsync(url);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Something has gone wrong in request{url}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }                     
        }

        private async Task<Tuple<int, string>> ParseResponseAsync(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();

            var beg = result.IndexOf('{');

            var end = result.LastIndexOf('}');

            var json = result.Substring(beg, end - beg + 1);

            var info = JsonConvert.DeserializeAnonymousType(json, new { sid = "", hash = "" });

            var sid = Convert.ToInt32(info.sid);

            return new Tuple<int, string>(sid!=0?sid:1,info.hash);
        }

        private async Task<Stream> DownloadAsync(string downloadUrl,INotifyProgressChanged onProgressChanged)
        {
            var result = await DependencyService.Get<IDownloader>().GetStreamAsync(downloadUrl,onProgressChanged);
            
            return result;
        }

    }
}
