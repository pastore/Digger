using Digger.Core.Interfaces;

namespace Digger.Core.Handlers
{
    public class CsHandler : BaseHandler
    {
        public CsHandler(IDiggerConfiguration diggerConfiguration, IFileService fileService) : base(diggerConfiguration, fileService) { }

        public override string Pattern => string.Join(".", base.Pattern, DiggerConfiguration.ActionType.ToString().ToLower());
    }
}
