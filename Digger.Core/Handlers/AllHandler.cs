using Digger.Core.Interfaces;

namespace Digger.Core.Handlers
{
    public class AllHandler : BaseHandler
    {
        public AllHandler(IDiggerConfiguration diggerConfiguration, IFileService fileService) : base(diggerConfiguration, fileService) { }
    }
}
