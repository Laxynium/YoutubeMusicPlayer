using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicManagement.Application.Commands
{
    public class AddSongFromYoutube : ICommand
    {
        public Guid SongId { get; }
        public string YtId { get; }
        public string Title { get; }
        public string Artist { get; }
        public string ThumbnailUrl { get; }

        public AddSongFromYoutube(Guid songId, string ytId, string title, string artist, string thumbnailUrl)
        {
            SongId = songId;
            YtId = ytId;
            Title = title;
            Artist = artist;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}