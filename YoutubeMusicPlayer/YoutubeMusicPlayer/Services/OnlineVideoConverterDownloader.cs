using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public class OnlineVideoConverterMusicDownloader:IDownloadService
    {

        private readonly HttpClient _client;

        public OnlineVideoConverterMusicDownloader(HttpClient client)
        {
            _client = client;
        }

        public event EventHandler<int> OnProgressChanged;

        public async Task<byte[]> DownloadMusicAsyncArray(string musicIdFromYoutube)
        {
            var downloadReponse = await GetDownloadResponse(musicIdFromYoutube);

            return await downloadReponse.Content.ReadAsByteArrayAsync();
        }

        public async Task<Stream> DownloadMusicAsync(string musicIdFromYoutube,IOnProgressChanged onProgressChanged2)
        {
            var downloadReponse = await GetDownloadResponse(musicIdFromYoutube);

            return await downloadReponse.Content.ReadAsStreamAsync();
        }

        private async Task<HttpResponseMessage> GetDownloadResponse(string musicIdFromYoutube)
        {
            string encodedYtUrl = $"https%3A%2F%2Fwww.youtube.com%2Fwatch%3Fv%3{musicIdFromYoutube}";

            _client.DefaultRequestHeaders.Clear();

            var requestConent = new StringContent
            (
                $"function=validate&args%5Bdummy%5D=1&args%5BurlEntryUser%5D={encodedYtUrl}&args%5BfromConvert%5D=urlconverter&args%5BrequestExt%5D=mp3&args%5BnbRetry%5D=0&args%5BvideoResolution%5D=-1&args%5BaudioBitrate%5D=0&args%5BaudioFrequency%5D=0&args%5Bchannel%5D=stereo&args%5Bvolume%5D=0&args%5BstartFrom%5D=-1&args%5BendTo%5D=-1&args%5Bcustom_resx%5D=-1&args%5Bcustom_resy%5D=-1&args%5BadvSettings%5D=false&args%5BaspectRatio%5D=-1",
                Encoding.UTF8,
                new MediaTypeHeaderValue("application/x-www-form-urlencoded").MediaType
            );

            var response = await _client.PostAsync("https://www3.onlinevideoconverter.com/webservice", requestConent);

            var msg = await response.Content.ReadAsStringAsync();

            var convertInfo = JsonConvert.DeserializeAnonymousType(msg, new { result = new { dPageId = "" } });

            var url = $"https://www.onlinevideoconverter.com/pl/success?id={convertInfo.result.dPageId}";

            var jsonResponse = await _client.GetAsync(url);

            var result = await jsonResponse.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();

            doc.LoadHtml(result);

            var link = doc.GetElementbyId("downloadq").Attributes["href"].Value;

            _client.DefaultRequestHeaders.Add("Accept", "audio/mpeg3");

            return await _client.GetAsync(link);
        }
    }
}
