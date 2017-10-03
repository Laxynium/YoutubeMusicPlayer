using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace YoutubeMusicPlayer.Models
{
    public class Music:INotifyProgressChanged
    {
        public event EventHandler<int> ProgressChanged;

        public double Value { get; set; }

        [PrimaryKey]
        public string VideoId { get; set; }

        public string Title { get; set; }

        public string ImageSource { get; set; }

        public string FilePath { get; set; }


        public void OnProgressChanged(int value)
        {
            ProgressChanged?.Invoke(this, value);
        }
    }
}
