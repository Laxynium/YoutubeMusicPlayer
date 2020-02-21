using System;

namespace YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube
{
    public static class SongDownloadEvents
    {
        public abstract class SongDownloadEvent
        {
            public Guid SongId { get; }
            protected SongDownloadEvent(Guid songId)
            {
                SongId = songId;
            }
        }

        public class SongDownloadStarted : SongDownloadEvent
        {
            public string Title { get; }
            public string ThumbnailUrl { get; }
            public SongDownloadStarted(Guid songId, string title, string thumbnailUrl)
                : base(songId)
            {
                Title = title;
                ThumbnailUrl = thumbnailUrl;
            }
        }

        public class SongDownloadProgressed : SongDownloadEvent
        {
            public int Progress { get; }

            public SongDownloadProgressed(Guid songId, int progress)
                : base(songId)
            {
                Progress = progress;
            }
        }

        public class SongDownloadFinished : SongDownloadEvent
        {
            public SongDownloadFinished(Guid songId)
                : base(songId)
            {
            }
        }

        public class SongDownloadFailed : SongDownloadEvent
        {
            public SongDownloadFailed(Guid songId) : base(songId)
            {
            }
        }
    }
}