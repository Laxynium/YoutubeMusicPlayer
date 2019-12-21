using System;
using YoutubeMusicPlayer.Domain.Framework;

namespace YoutubeMusicPlayer.Domain.MusicManagement.Commands
{
    public class AddSongToAllSongsPlaylist : ICommand
    {
        public Guid Id { get; }

        public AddSongToAllSongsPlaylist(Guid id)
        {
            Id = id;
        }
    }
}