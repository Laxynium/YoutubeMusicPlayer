using System;
using YoutubeMusicPlayer.Framework.BuildingBlocks;
using YoutubeMusicPlayer.MusicManagement.Domain.ValueObjects;

namespace YoutubeMusicPlayer.MusicManagement.Domain.Entities
{
    public class Song : Entity<SongId>
    {
        public string YtId { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public SongPath SongPath { get; private set; }
        public string ThumbnailUrl { get; private set; }

        private Song():base(new SongId(Guid.NewGuid())) { }

        public Song(SongId id, string ytId, string title, string artist, SongPath songPath, string thumbnailUrl) : base(id)
        {
            YtId = ytId;
            Title = title;
            Artist = artist;
            SongPath = songPath;
            ThumbnailUrl = thumbnailUrl;
        }

        public void ChangeTitle(string title)
        {
            Title = title;
        }

        public void ChangeArtist(string artist)
        {
            Artist = artist;
        }
    }
}