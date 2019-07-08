using System;
using SQLite;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class Song
    {
        //TODO there shoudnt be public setters, but it makes easier persisting this object
        [PrimaryKey]
        public Guid Id { get; set; }
        public string YoutubeId { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }
        public string FilePath { get; set; }

        /// <summary>
        /// Do not use this constructor. It only exists so could be stored.
        /// </summary>
        public Song()
        {

        }

        public Song(string youtubeId, string title, string imageSource, string filePath)
        {
            Id = Guid.NewGuid();
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }

        public Song(Guid id, string youtubeId, string title, string imageSource, string filePath)
        {
            Id = id;
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
            FilePath = filePath;
        }
    }
}