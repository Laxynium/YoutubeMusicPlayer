using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace YoutubeMusicPlayer.Framework
{
    public class FileManager
    {
        private readonly IList<Func<Task>> _rollback
            = new List<Func<Task>>();
        public async Task<Result> CreateAsync(string filePath, byte[] content)
        {
            var result = await Result.Try(async () => await File.WriteAllBytesAsync(filePath, content));

            result.Tap(() => _rollback.Add(() =>
            {
                File.Delete(filePath);
                return Task.CompletedTask;
            }));

            return result;
        }

        public async Task<Result> DeleteAsync(string filePath)
        {
            var bytes = await File.ReadAllBytesAsync(filePath);

            var result = Result.Try(()=>File.Delete(filePath));

            result.Tap(() => _rollback.Add(async () => await File.WriteAllBytesAsync(filePath, bytes)));

            return result;
        }

        public async Task<Result> Rollback()
        {
            Result result = Result.Ok();

            foreach (var rollback in _rollback.Reverse())
            {
                result = Result.Combine(result,await Result.Try(async()=> await rollback.Invoke()));
            }

            return result;
        }
    }
}