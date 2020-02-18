namespace YoutubeMusicPlayer.MusicManagement.Application.Services.Youtube
{
    public class YoutubeSongDto
    {
        public string YtId { get; }
        public byte[] Data { get; }

        public YoutubeSongDto(string ytId, byte[] data)
        {
            YtId = ytId;
            Data = data;
        }
    }
}