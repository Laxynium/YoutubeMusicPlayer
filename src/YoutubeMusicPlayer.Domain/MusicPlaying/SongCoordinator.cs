using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeMusicPlayer.Domain.SharedKernel;

namespace YoutubeMusicPlayer.Domain.MusicPlaying
{
    public class SongCoordinator
    {
        private readonly Queue<SongId> _queue = new Queue<SongId>();
        public SongId CurrentlySelected { get; private set; }
        private  List<SongId> _playlist;
        private int _pointerPosition = 0;
        private bool _isSongIdFromPlaylist = true;
        public SongCoordinator(IEnumerable<SongId> playlist)
        {
            _playlist = playlist?.ToList() ?? throw new ArgumentNullException(nameof(playlist));
            //if(!playlist.Any()) throw new ArgumentException($"{nameof(playlist)} cannot be empty list.");
            if (_playlist.Any())
            {
                CurrentlySelected = _playlist[0];
            }
        }

        public void ChangePlaylist(List<SongId> newPlaylist)
        {
            //if(!newPlaylist?.Any() ?? throw new ArgumentException(nameof(newPlaylist)))
            //    throw new ArgumentException($"{nameof(newPlaylist)} cannot be empty list.");

            _playlist = newPlaylist;
            _pointerPosition = 0;
            if (_isSongIdFromPlaylist)
            {
                CurrentlySelected = _playlist.Any() ? _playlist[_pointerPosition] : null;
            }
        }

        public void Enqueue(SongId songId)
        {
            _queue.Enqueue(songId);
        }

        public void GoToPrevious()
        {
            if (!_queue.Any())
            {
                int nextPointerPosition = (_playlist.Count + _pointerPosition - 1) % _playlist.Count;
                CurrentlySelected = _playlist[nextPointerPosition];
                _pointerPosition = nextPointerPosition;
            }
            else 
            {
                if (!_playlist.Any())
                    return;
                if (_isSongIdFromPlaylist)
                {
                    int nextPointerPosition = (_playlist.Count + _pointerPosition - 1) % _playlist.Count;
                    CurrentlySelected = _playlist[nextPointerPosition];
                    _pointerPosition = nextPointerPosition;
                }
                else
                {
                    CurrentlySelected = _playlist[_pointerPosition];
                }

            }
        }

        public void GoToNext()
        {
            if (_queue.Any())
                GoToNextElementOnQueue();
            else
                GoToNextElementOnPlaylist();
        }

        private void GoToNextElementOnQueue()
        {
            //here I know that queue is not empty
            CurrentlySelected = _queue.Dequeue();
            _isSongIdFromPlaylist = false;
        }

        private void GoToNextElementOnPlaylist()
        {
            if (!_playlist.Any())
                return;

            int nextPointerPosition = (_pointerPosition + 1) % _playlist.Count;
            CurrentlySelected = _playlist[nextPointerPosition];
            _pointerPosition = nextPointerPosition;
            _isSongIdFromPlaylist = true;
        }
    }
}