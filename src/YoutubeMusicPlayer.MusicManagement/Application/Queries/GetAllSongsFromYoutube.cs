using System;
using System.Collections.Generic;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicManagement.Application.Queries
{
    public class SongDto
    {
        public Guid SongId { get; }
        public string YtId { get; }
        public string Title { get; }
        public string ThumbnailUrl { get; }

        public SongDto(Guid songId, string ytId, string title, string thumbnailUrl)
        {
            SongId = songId;
            YtId = ytId;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
        }
    }
    public class GetAllSongsFromYoutube : IQuery<IEnumerable<SongDto>>
    {
    }
}