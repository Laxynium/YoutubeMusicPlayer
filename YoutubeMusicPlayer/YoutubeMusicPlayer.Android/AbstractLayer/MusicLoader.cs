using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Java.IO;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using YoutubeMusicPlayer.Models;
using YoutubeMusicPlayer.Repositories;
using Environment = Android.OS.Environment;
using File = Java.IO.File;

[assembly: Dependency(typeof(MusicLoader))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    //public class MusicFilesFilter : IFileFilter
    //{
    //    private readonly IList<string> _fileExtensions = new List<string>
    //    {
    //        "mp3"
    //    };
    //    public bool Accept(File pathname)
    //    {
    //        string extension = Path.GetExtension(pathname.Name);
    //        if (pathname.IsFile && _fileExtensions.Contains(extension))
    //            return true;
    //        return false;
    //    }

    //    public void Dispose()
    //    {

    //    }

    //    public IntPtr Handle { get; }
    //}

    //public class DirectoryFilter : IFileFilter
    //{
    //    public bool Accept(File pathname)
    //    {
    //        if (pathname.IsDirectory)
    //            return true;
    //        return false;
    //    }
    //    public void Dispose()
    //    {

    //    }

    //    public IntPtr Handle { get; }

    //}

    public class MusicLoader : IMusicLoader
    {
        private readonly IMusicRepository _repository;

        public MusicLoader(IMusicRepository repository)
        {
            _repository = repository;
            _repository.InitializeAsync();
        }
        private readonly IList<string> _fileExtensions = new List<string>
        {
            ".mp3"
        };
        public async Task LoadMusic()
        {
            await Task.Run(() =>
            {
                var storage = Environment.ExternalStorageDirectory;

                var files = Directory.GetFiles(storage.AbsolutePath).Select(x => new File(x)).ToList();
                files.AddRange(Directory.GetDirectories(storage.AbsolutePath).Select(x => new File(x)));

                Queue<File> queue = new Queue<File>(files);
                List<File> musicFiles = new List<File>();

                while (queue.Count > 0)
                {
                    var file = queue.Dequeue();
                    if (file.IsDirectory)
                    {
                        Directory.GetFiles(file.AbsolutePath).Select(x => new File(x)).ToList().ForEach(x =>
                        {
                            queue.Enqueue(x);
                        });
                        Directory.GetDirectories(file.AbsolutePath).Select(x => new File(x)).ToList().ForEach(x =>
                        {
                            queue.Enqueue(x);
                        });
                    }
                    else if (file.IsFile && _fileExtensions.Contains(Path.GetExtension(file.AbsolutePath)))
                    {
                        musicFiles.Add(file);
                    }
                }

                musicFiles = musicFiles.Distinct().ToList();
                musicFiles.ForEach(async x =>
                {
                    var music = new Music
                    {
                        FilePath = x.AbsolutePath,
                        ImageSource = "",
                        Title = x.Name,
                        VideoId = "lM"+x.AbsolutePath.GetHashCode().ToString(),
                        Value = 1
                    };
                    await _repository.AddAsync(music);
                });
            });          
        }
    }
}