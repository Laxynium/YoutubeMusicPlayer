using System;
using YoutubeMusicPlayer.Framework.Messaging;

namespace YoutubeMusicPlayer.MusicManagement.Commands
{
    public class AddSongToMasterPlaylistCommand : ICommand
    {
        public Guid SongId { get; }

        public AddSongToMasterPlaylistCommand(Guid songId)
        {
            SongId = songId;
        }
    }
}