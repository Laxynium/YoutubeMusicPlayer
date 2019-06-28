using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YoutubeMusicPlayer.Models;

namespace YoutubeMusicPlayer.Services
{
    public class YoutubeService : IYoutubeService
    {
        private string _url = "https://www.youtube.com/results?search_query={0}&pbj=1";

        public async Task<IEnumerable<Music>> FindMusicAsync(string title)
        {
            var client = new HttpClient();
            var uri = $"https://www.youtube.com/results?search_query={title}&pbj=1";
            client.DefaultRequestHeaders.Add("Host", "www.youtube.com");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("X-YouTube-Client-Name", "1");
            client.DefaultRequestHeaders.Add("X-YouTube-Client-Version", "2.20190626");
            client.DefaultRequestHeaders.Add("Cookie", "VISITOR_INFO1_LIVE=cIdr9kyAxzo; YSC=uiQyEuiTVYU; PREF=f1=50000000; GPS=1; CONSENT=WP.27b70d; ST-13r0l79=oq=nadchodzi%20lato&gs_l=youtube.3..0i71k1l10.0.0.1.431792.0.0.0.0.0.0.0.0..0.0....0...1ac..64.youtube..0.0.0....0.HCy0eUqhZUA&feature=web-masthead-search&itct=CB4Q7VAiEwjau5D-oorjAhWOOZsKHX10AT0o9CQ%3D&csn=bAgVXdrVCI7z7AT96IXoAw");
            client.DefaultRequestHeaders.Add("Accept", @"*/*");


            var response = await client.GetAsync(String.Format(_url,title));
            var result = await response.Content.ReadAsStringAsync();


            var type = new[]
            {
                new
                {
                    response = new
                    {
                        contents=new
                        {
                            twoColumnSearchResultsRenderer=new
                            {
                                primaryContents=new
                                {
                                    sectionListRenderer = new
                                    {
                                        contents = new[]
                                        {
                                            new
                                            {
                                                itemSectionRenderer=new
                                                {
                                                    contents=new[]
                                                    {
                                                        new
                                                        {
                                                            videoRenderer=new
                                                            {
                                                                videoId="",
                                                                thumbnail=new
                                                                {
                                                                    thumbnails=new[]
                                                                    {
                                                                        new{url=""}
                                                                    }
                                                                },
                                                                title=new
                                                                {
                                                                    runs=new[]
                                                                    {
                                                                        new {text=""}
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var newObject = JsonConvert.DeserializeAnonymousType(result, type);

            var musicItems = new List<Music>();

            var contents = newObject[1].response.contents.twoColumnSearchResultsRenderer
                .primaryContents.sectionListRenderer.contents[0].itemSectionRenderer.contents;

            foreach (var content in contents)
            {
                var item = new Music
                {
                    Title = content?.videoRenderer?.title?.runs[0]?.text,
                    VideoId = content?.videoRenderer?.videoId,
                    ImageSource = content?.videoRenderer?.thumbnail?.thumbnails[0]?.url
                };

                if (item.Title == null || item.VideoId == null) continue;

                musicItems.Add(item);
            }

            client.Dispose();

            return musicItems;
        }
    }
}