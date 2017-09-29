using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Models
{
    public interface IOnProgressChanged
    {
        event EventHandler<int> ProgressChanged;

        void OnProgressChanged(int value);

        double Value { get; set; }
    }
}
