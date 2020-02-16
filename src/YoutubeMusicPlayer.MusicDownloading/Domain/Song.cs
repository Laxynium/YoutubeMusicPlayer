﻿using System;

namespace YoutubeMusicPlayer.MusicDownloading.Domain
{
    public class Song
    {
        public Guid Id { get; private set; }
        public string FilePath { get; private set; }
        public string YtId { get; private set; }
        public string Title { get; private set; }
        public string ImageSource { get; private set; }

        private Song() { }

        public Song(Guid id,string filePath, string ytId, string title, string imageSource)
        {
            Id = id;
            FilePath = filePath;
            YtId = ytId;
            Title = title;
            ImageSource = imageSource;
        }
    }
}