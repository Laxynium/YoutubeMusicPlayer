using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YoutubeMusicPlayer.Customs
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
