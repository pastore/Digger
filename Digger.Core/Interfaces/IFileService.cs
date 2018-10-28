using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Digger.Core.Interfaces
{
    public interface IFileService
    {
        Task DigFolderAsync(DirectoryInfo directory, string pattern, Func<string, string> transformFile, ConcurrentQueue<string> queue, CancellationToken cancellationToken);

        Task WriteFilesToFileAsync(List<string> filesList, string resultFilePath, CancellationToken cancellationToken);
    } 
}
