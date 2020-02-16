using System;
using YoutubeMusicPlayer.MusicDownloading.Domain;

namespace YoutubeMusicPlayer.MusicDownloading.ReadModel
{
    public class SongDto
    {
        public Guid? Id { get; private set; }
        public string YtId { get; private set; }
        public string Title { get; private set; }
        public string ImageSource { get; private set; }
        public double Progress { get; private set; }

        public SongDto(string ytId, string title, string imageSource)
            : this((Guid?) null, ytId, title, imageSource, 0)
        {

        }
        public SongDto(Guid? id, string ytId, string title, string imageSource, double progress)
        {
            Id = id;
            YtId = ytId;
            Title = title;
            ImageSource = imageSource;
            Progress = progress;
        }

        public SongDto WithProgress(double progress)
        {
            return new SongDto(null,YtId,Title,ImageSource,progress);
        }

        public SongDto WithId(Guid id)
        {
            return new SongDto(id, YtId, Title, ImageSource, Progress);
        }
    }
}