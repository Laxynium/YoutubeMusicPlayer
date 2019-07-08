using System;

namespace YoutubeMusicPlayer.Domain.MusicDownloading
{
    public class SongPath
    {
        public string Value { get; }

        public SongPath(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"Path cannot be empty.");
            Value = path;
        }

        public static implicit operator string(SongPath path)
        {
            return path.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}