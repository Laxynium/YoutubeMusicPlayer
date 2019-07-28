using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeMusicPlayer.AbstractLayer;
using YoutubeMusicPlayer.Domain.MusicDownloading;
using YoutubeMusicPlayer.Domain.MusicDownloading.Repositories;
using YoutubeMusicPlayer.Droid.AbstractLayer;
using Environment = Android.OS.Environment;
using File = Java.IO.File;

[assembly: Dependency(typeof(MusicLoader))]
namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    //TODO remove
    public class MusicLoader : IMusicLoader
    {
        private readonly ISongRepository _repository;

        public MusicLoader(ISongRepository repository)
        {
            _repository = repository;
        }
        private readonly IList<string> _fileExtensions = new List<string>
        {
            ".mp3"
        };
        public async Task LoadMusic()
        {
            //await Task.Run(() =>
            //{
            //    var storage = Environment.ExternalStorageDirectory;

            //    var files = Directory.GetFiles(storage.AbsolutePath).Select(x => new File(x)).ToList();
            //    files.AddRange(Directory.GetDirectories(storage.AbsolutePath).Select(x => new File(x)));

            //    Queue<File> queue = new Queue<File>(files);
            //    List<File> musicFiles = new List<File>();

            //    while (queue.Count > 0)
            //    {
            //        var file = queue.Dequeue();
            //        if (file.IsDirectory)
            //        {
            //            Directory.GetFiles(file.AbsolutePath).Select(x => new File(x)).ToList().ForEach(x =>
            //            {
            //                queue.Enqueue(x);
            //            });
            //            Directory.GetDirectories(file.AbsolutePath).Select(x => new File(x)).ToList().ForEach(x =>
            //            {
            //                queue.Enqueue(x);
            //            });
            //        }
            //        else if (file.IsFile && _fileExtensions.Contains(Path.GetExtension(file.AbsolutePath)))
            //        {
            //            musicFiles.Add(file);
            //        }
            //    }

            //    musicFiles = musicFiles.Distinct().ToList();
            //    musicFiles.ForEach(async x =>
            //    {
            //        var music = new Song
            //        {
            //            FilePath = x.AbsolutePath,
            //            ImageSource = "",
            //            Title = x.Name,
            //            Id = Guid.NewGuid()
            //        };
            //        await _repository.AddAsync(music);
            //    });
            //});          
        }
    }
}