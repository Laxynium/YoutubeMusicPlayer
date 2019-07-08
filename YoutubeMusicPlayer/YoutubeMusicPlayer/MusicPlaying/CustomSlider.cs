using System;
using Xamarin.Forms;

namespace YoutubeMusicPlayer.MusicPlaying
{
    public class CustomSlider:Slider
    {
        public event EventHandler<double> ProgressArranged;

        public void OnProgressArranged(double newProgressValue)
        {
            ProgressArranged?.Invoke(this,newProgressValue);
        }
    }
}
