using System;
using System.Collections.Generic;
using System.Linq;

namespace YoutubeMusicPlayer.Domain.MusicPlaying
{
    public class SongCoordinator
    {
        private readonly Queue<Song> _queue = new Queue<Song>();
        public Song CurrentlySelected { get; private set; }
        private  List<Song> _playlist;
        private int _pointerPosition = 0;
        private bool _isSongFromPlaylist = true;
        public SongCoordinator(List<Song> playlist)
        {
            _playlist = playlist ?? throw new ArgumentNullException(nameof(playlist));
            if(!playlist.Any()) throw new ArgumentException($"{nameof(playlist)} cannot be empty list.");
            CurrentlySelected = _playlist[0];
        }

        public void ChangePlaylist(List<Song> newPlaylist)
        {
            if(!newPlaylist?.Any() ?? throw new ArgumentException(nameof(newPlaylist)))
                throw new ArgumentException($"{nameof(newPlaylist)} cannot be empty list.");

            _playlist = newPlaylist;
            _pointerPosition = 0;
            if (_isSongFromPlaylist)
            {
                CurrentlySelected = _playlist[_pointerPosition];
            }
        }

        public void Enqueue(Song song)
        {
            _queue.Enqueue(song);
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
                if (_isSongFromPlaylist)
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
            _isSongFromPlaylist = false;
        }

        private void GoToNextElementOnPlaylist()
        {
            int nextPointerPosition = (_pointerPosition + 1) % _playlist.Count;
            CurrentlySelected = _playlist[nextPointerPosition];
            _pointerPosition = nextPointerPosition;
            _isSongFromPlaylist = true;
        }
    }
}