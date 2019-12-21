using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using YoutubeMusicPlayer.Droid.CustomRenderers;
using YoutubeMusicPlayer.MusicPlaying;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(CustomSliderRenderer))]
namespace YoutubeMusicPlayer.Droid.CustomRenderers
{
    class CustomSliderRenderer : SliderRenderer
    {
        public CustomSliderRenderer(Context context):base(context)
        {
            
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);

            if (Control == null) return;

            Control.ProgressDrawable.SetColorFilter(Color.LimeGreen.ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcIn);
            Control.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(Color.LimeGreen.ToAndroid());
            Control.ScaleY = 1.4f;

            Control.Touch += Control_Touch;
        }

        private void Control_Touch(object sender, TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Up)
            {
                var slider = Element as CustomSlider;

                slider?.OnProgressArranged(slider.Value);

                e.Handled = false;

            }
            else if (e.Event.Action == MotionEventActions.Move)
                e.Handled = false;
        }
    }
}
