using System;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicManagement.Commands
{
    public class RemoveSongFromAllSongsPlaylist : ICommand
    {
        public Guid Id { get; }

        public RemoveSongFromAllSongsPlaylist(Guid id)
        {
            Id = id;
        }
    }
}