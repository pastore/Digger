using Digger.Core.Interfaces;
using Digger.Core.Utils;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Digger.Core.Handlers
{
    public abstract class BaseHandler
    {
        private readonly IFileService _fileService;
        protected readonly IDiggerConfiguration DiggerConfiguration;
        
        private readonly ConcurrentQueue<string> _fileQueue;

        protected BaseHandler(IDiggerConfiguration diggerConfiguration, IFileService fileService)
        {
            DiggerConfiguration = diggerConfiguration;
            _fileService = fileService;
            _fileQueue = new ConcurrentQueue<string>();
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            await _fileService.DigFolderAsync(new DirectoryInfo(DiggerConfiguration.RootFolder), Pattern,
                TransformFile, _fileQueue, cancellationToken).Then(async (x) =>
                {
                    await _fileService.WriteFilesToFileAsync(_fileQueue.ToList(), DiggerConfiguration.ResultFilePath,
                       cancellationToken);
                });
        }

        public virtual string Pattern { get; set; } = "*";

        public virtual string TransformFile(string filePath)
        {
            var filePathSegments = filePath.Split('\\').ToList();
            var rootFolderSegments = DiggerConfiguration.RootFolder.Split('\\');
            var indexRootFolder = filePathSegments.IndexOf(rootFolderSegments[rootFolderSegments.Length - 1]);

            return @"\" + string.Join(@"\", filePathSegments.Skip(indexRootFolder + 1));
        }
    }
}
