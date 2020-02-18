using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicManagement.Application.Commands
{
    public class RemoveSong : ICommand
    {
        public Guid SongId { get; }

        public RemoveSong(Guid songId)
        {
            SongId = songId;
        }
    }
}