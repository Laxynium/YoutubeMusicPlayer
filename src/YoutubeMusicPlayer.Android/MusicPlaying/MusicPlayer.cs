using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Media;
using Java.IO;
using YoutubeMusicPlayer.Droid.MusicPlaying;
using YoutubeMusicPlayer.MusicPlaying;
using Uri = Android.Net.Uri;
[assembly: Xamarin.Forms.Dependency(typeof(MusicPlayer))]
namespace YoutubeMusicPlayer.Droid.MusicPlaying
{
    public class MusicPlayer:IMusicPlayer
    {
        private enum PlayerAction
        {
            Play,Pause,Stop
        }
        
        private readonly MediaPlayer _mediaPlayer;

        private bool IsPrepared { get; set; }

        private IDictionary<PlayerAction, EventHandler> Actions { get; set; }

        public event EventHandler PlaybackCompleted;

        public event EventHandler<int> ProgressChanged;

        public MusicPlayer()
        {
            _mediaPlayer = new MediaPlayer();

            Actions = new Dictionary<PlayerAction, EventHandler>
            {
                [PlayerAction.Play] = (s, a) =>
                {
                    _mediaPlayer.Start();
                    Task.Run(() => OnProgressChanged(0));
                },
                [PlayerAction.Pause] = (s, a) =>_mediaPlayer.Pause(),
                [PlayerAction.Stop] = (s, a) =>_mediaPlayer.Stop()
            };

            _mediaPlayer.Prepared += OnPrepared;

            _mediaPlayer.Completion += OnComplete;
       
        }


        private void OnComplete(object sender, EventArgs e)
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

            await _mediaPlayer.SetDataSourceAsync(Xamarin.Forms.Forms.Context, Uri.FromFile(new File(fileUrl)));

            IsPrepared = false;
           
            Actions.Values.ToList().ForEach(x => _mediaPlayer.Prepared -= x);

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
            }, Actions[PlayerAction.Play]);
        }

        public async Task Pause()
        {
            if (!_mediaPlayer.IsPlaying)
                return;

            PrepareAction(_mediaPlayer.Pause, Actions[PlayerAction.Pause]);
        }

        public async Task Stop()
        {
            if (!_mediaPlayer.IsPlaying)
                return;

            PrepareAction(_mediaPlayer.Stop, Actions[PlayerAction.Stop]);
        }

        private void PrepareAction(Action action, EventHandler handler)
        {
            if (IsPrepared)
                action.Invoke();
            else
                _mediaPlayer.Prepared += handler;
        }
        private void OnPrepared(object sender, EventArgs e)
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
    }
}