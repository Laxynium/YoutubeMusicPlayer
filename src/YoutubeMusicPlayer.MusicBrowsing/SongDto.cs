namespace YoutubeMusicPlayer.MusicBrowsing
{
    public class SongDto
    {
        public string YoutubeId { get; }
        public string Title { get; }
        public string ThumbnailUrl { get; }

        public SongDto(string youtubeId, string title, string thumbnailUrl)
        {
            YoutubeId = youtubeId;
            Title = title;
            ThumbnailUrl = thumbnailUrl;
        }
    }
}