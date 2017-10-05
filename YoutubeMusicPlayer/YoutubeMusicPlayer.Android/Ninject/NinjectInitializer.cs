using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Ninject;

namespace YoutubeMusicPlayer.Droid.Ninject
{
    public static class NinjectInitializer
    {
        public static IKernel Kernel { get; private set; }
        public static void Initialize()
        {
            Kernel = new StandardKernel(new YoutubeMusicPlayerModule());
        }
    }
}