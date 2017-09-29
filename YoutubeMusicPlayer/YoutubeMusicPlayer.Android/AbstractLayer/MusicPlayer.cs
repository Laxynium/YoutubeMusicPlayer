using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Media;
using Java.IO;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using Uri = Android.Net.Uri;

[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class MusicPlayer:IMusicPlayer
    {
        private readonly MediaPlayer _mediaPlayer;

        private bool IsPrepared { get; set; }

        private List<EventHandler> Actions { get; set; }

        public MusicPlayer()
        {
            _mediaPlayer = new MediaPlayer();

            Actions=new List<EventHandler>
            {
                (s, a) => _mediaPlayer.Start(),
                (s, a) =>_mediaPlayer.Pause(),
                (s, a) =>_mediaPlayer.Stop()
            };

            _mediaPlayer.Prepared += _mediaPlayer_Prepared;
        }

        public async Task SetSource(string fileUrl)
        {
            _mediaPlayer.Reset();

            await _mediaPlayer.SetDataSourceAsync(Xamarin.Forms.Forms.Context,Uri.FromFile(new File(fileUrl)));

            IsPrepared = false;
           
            Actions.ForEach(x => _mediaPlayer.Prepared -= x);

            _mediaPlayer.PrepareAsync();
        }

        public async Task Play()
        {
            PrepareAction(_mediaPlayer.Start, Actions[0]);
        }
        public async Task Pause()
        {
            if (!_mediaPlayer.IsPlaying)
                return;

            PrepareAction(_mediaPlayer.Pause, Actions[1]);
        }

        public async Task Stop()
        {
            if (!_mediaPlayer.IsPlaying)
                return;

            PrepareAction(_mediaPlayer.Stop, Actions[2]);
        }

        private void PrepareAction(Action action, EventHandler handler)
        {
            if (IsPrepared)
                action.Invoke();
            else
                _mediaPlayer.Prepared += handler;
        }
        private void _mediaPlayer_Prepared(object sender, EventArgs e)
        {
            IsPrepared = true;
        }
    }
}