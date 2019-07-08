namespace YoutubeMusicPlayer.Domain.MusicSearching
{
    public class MusicDto
    {
        public string YoutubeId { get; }

        public string Title { get; }

        public string ImageSource { get; }


        public MusicDto(string youtubeId, string title, string imageSource)
        {
            YoutubeId = youtubeId;
            Title = title;
            ImageSource = imageSource;
        }
    }
}
