using Digger.Core.Interfaces;
using System.Linq;

namespace Digger.Core.Handlers
{
    public class Reversed2Handler : BaseHandler
    {
        public Reversed2Handler(IDiggerConfiguration diggerConfiguration, IFileService fileService) : base(diggerConfiguration, fileService) { }

        public override string TransformFile(string filePath)
        {
            return string.Join(@"\", base.TransformFile(filePath).Split('\\').Select(x => string.Join("", x.ToCharArray().Reverse().ToArray())).Reverse());
        }
    }
}
