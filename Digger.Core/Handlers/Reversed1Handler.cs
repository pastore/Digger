using Digger.Core.Interfaces;
using System.Linq;

namespace Digger.Core.Handlers
{
    public class Reversed1Handler : BaseHandler
    {
        public Reversed1Handler(IDiggerConfiguration diggerConfiguration, IFileService fileService) : base(diggerConfiguration, fileService) { }

        public override string TransformFile(string filePath)
        {
            return string.Join(@"\", base.TransformFile(filePath).Split('\\').Reverse());
        }
    }
}
