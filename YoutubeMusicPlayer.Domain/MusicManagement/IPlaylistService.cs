using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Domain.MusicManagement
{
    public interface IPlaylistService
    {
        Task Create(string name, IEnumerable<Guid> songIds = null);
        Task UpdateName(Guid playlistId, string newName);
        Task AddSongToExisting(Guid playlistId, Guid songId);
        Task RemoveSongFromExisting(Guid playlistId, Guid songId, int position);
        Task Remove(Guid playlistId);
        Task AddSongsToExisting(Guid playlistId, IEnumerable<Guid> songIds);
        Task RemoveSongsFromExisting(Guid playlistId, IEnumerable<Guid> songIds);
    }
}