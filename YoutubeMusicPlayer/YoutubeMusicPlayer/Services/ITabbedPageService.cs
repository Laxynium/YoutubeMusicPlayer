using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeMusicPlayer.Services
{
    public interface ITabbedPageService
    {
        Task ChangePage(int index);
    }
}
