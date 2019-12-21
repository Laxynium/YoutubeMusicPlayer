using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicDownloading.Application.Commands
{
    public class RemoveSongCommand : ICommand
    {
        public Guid SongIdGuid { get; }

        public RemoveSongCommand(Guid songIdGuid)
        {
            SongIdGuid = songIdGuid;
        }
    }
}