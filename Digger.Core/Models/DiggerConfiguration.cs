using Digger.Core.Enums;
using Digger.Core.Interfaces;

namespace Digger.Core.Models
{
    public class DiggerConfiguration: IDiggerConfiguration
    {
        public string RootFolder { get; set; }
        public DiggerActionType ActionType { get; set; }
        public string ResultFilePath { get; set; }
    }
}
