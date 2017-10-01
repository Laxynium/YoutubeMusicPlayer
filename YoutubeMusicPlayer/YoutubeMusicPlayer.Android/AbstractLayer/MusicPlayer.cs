using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public event EventHandler PlaybackCompleted;

        public event EventHandler<int> ProgressChanged;

        public MusicPlayer()
        {
            _mediaPlayer = new MediaPlayer();

            Actions = new List<EventHandler>
            {
                (s, a) =>
                {
                    _mediaPlayer.Start();
                    Task.Run(() => OnProgressChanged(0));
                },
                (s, a) =>_mediaPlayer.Pause(),
                (s, a) =>_mediaPlayer.Stop()
            };

            _mediaPlayer.Prepared += _mediaPlayer_Prepared;

            _mediaPlayer.Completion += _mediaPlayer_Completion;
       
        }


        private void _mediaPlayer_Completion(object sender, EventArgs e)
        {
            PlaybackCompleted?.Invoke(sender,e);
        }

        public async Task SetProgressAsync(double percentOfLength)
        {
            if (!IsPrepared)
                return;

            int msec = (int)(_mediaPlayer.Duration * percentOfLength / 100f);

           _mediaPlayer.SeekTo(msec);
        }

        public async Task SetSourceAsync(string fileUrl)
        {
            _mediaPlayer.Reset();

            await _mediaPlayer.SetDataSourceAsync(Xamarin.Forms.Forms.Context,Uri.FromFile(new File(fileUrl)));

            IsPrepared = false;
           
            Actions.ForEach(x => _mediaPlayer.Prepared -= x);

            _mediaPlayer.PrepareAsync();
        }

        public async Task PlayAsync()
        {
            if (_mediaPlayer.IsPlaying)
                return;

            PrepareAction(()=>
            {
                _mediaPlayer.Start();
                Task.Run(() => OnProgressChanged(0));
            }, Actions[0]);
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

       
        public async void OnProgressChanged(int value)
        {
            if (!_mediaPlayer.IsPlaying)
            {
                return;
            }
            var pos = _mediaPlayer.CurrentPosition;
            var length = _mediaPlayer.Duration;
           
            var progress = (int)(((double)pos / length) * 100);

            ProgressChanged?.Invoke(this, progress);

            await Task.Delay(1000);

            OnProgressChanged(0);
        }

        public double Value { get; set; }
    }
}