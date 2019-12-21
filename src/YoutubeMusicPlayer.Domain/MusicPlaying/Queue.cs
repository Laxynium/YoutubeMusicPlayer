using System.Collections.Generic;

namespace YoutubeMusicPlayer.Domain.MusicPlaying
{
    internal class Queue
    {
        private IList<Song> _songs;

        bool IsEmpty()
        {
            return false;
        }

        public void EnqueueRange(IEnumerable<Song> songs)
        {

        }

        public void Enqueue(Song song)
        {

        }

        public void Dequeue(Song song)
        {

        }
    }
}