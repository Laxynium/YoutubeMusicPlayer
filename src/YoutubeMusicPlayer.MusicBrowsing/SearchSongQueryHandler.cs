using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicBrowsing
{
    public class SearchSongQueryHandler : IQueryHandler<SearchSongQuery,IReadOnlyList<SongDto>>
    {
        private readonly HttpClient _client;

        private string Url(string songTitle)=>$"https://www.youtube.com/results?search_query={songTitle}&pbj=1";

        public SearchSongQueryHandler()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Host", "www.youtube.com");
            _client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _client.DefaultRequestHeaders.Add("X-YouTube-Client-Name", "1");
            _client.DefaultRequestHeaders.Add("X-YouTube-Client-Version", "2.20190626");
            _client.DefaultRequestHeaders.Add("Cookie", "VISITOR_INFO1_LIVE=cIdr9kyAxzo; YSC=uiQyEuiTVYU; PREF=f1=50000000; GPS=1; CONSENT=WP.27b70d; ST-13r0l79=oq=nadchodzi%20lato&gs_l=youtube.3..0i71k1l10.0.0.1.431792.0.0.0.0.0.0.0.0..0.0....0...1ac..64.youtube..0.0.0....0.HCy0eUqhZUA&feature=web-masthead-search&itct=CB4Q7VAiEwjau5D-oorjAhWOOZsKHX10AT0o9CQ%3D&csn=bAgVXdrVCI7z7AT96IXoAw");
            _client.DefaultRequestHeaders.Add("Accept", @"*/*");
        }

        public async Task<IReadOnlyList<SongDto>> HandleAsync(SearchSongQuery query)
        {
            var response = await _client.GetAsync(Url(query.MusicTitle));
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

            var contents = newObject[1].response.contents.twoColumnSearchResultsRenderer
                .primaryContents.sectionListRenderer.contents[0].itemSectionRenderer.contents;

            var musicItems = contents.Select(content => new SongDto(
                    content?.videoRenderer?.videoId,
                    content?.videoRenderer?.title?.runs[0]?.text,
                    content?.videoRenderer?.thumbnail?.thumbnails[0]?.url))
                .Where(item => item.Title != null && item.YoutubeId != null)
                .ToList();

            return musicItems;
        }
    }
}