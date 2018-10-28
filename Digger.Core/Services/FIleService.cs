using Digger.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Digger.Core.Services
{
    public class FileService : IFileService
    {
        public async Task DigFolderAsync(DirectoryInfo directory, string pattern, Func<string, string> transformFile, ConcurrentQueue<string> queue,
            CancellationToken cancellationToken)
        {
            foreach (var result in directory.GetFiles(pattern).Select(file => file.FullName))
            {
                if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
                queue.Enqueue(transformFile(result));
            }
            
            await Task.WhenAll(
                directory
                .GetDirectories()
                .Select(dir => Task.Run(() => DigFolderAsync(dir, pattern, transformFile, queue, cancellationToken), cancellationToken)).ToArray()
            );
        }

        public async Task WriteFilesToFileAsync(List<string> filesList, string resultFilePath, CancellationToken cancellationToken)
        {
            using (var writer = File.OpenWrite(resultFilePath))
            {
                using (var streamWriter = new StreamWriter(writer))
                {
                    foreach (var file in filesList)
                    {
                        if (cancellationToken.IsCancellationRequested) throw new TaskCanceledException();
                        await streamWriter.WriteLineAsync(file);
                    }
                }
            };
        }
    }
}
