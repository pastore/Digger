using Digger.Core.Enums;

namespace Digger.Core.Interfaces
{
    public interface IDiggerConfiguration
    {
        string RootFolder { get; set; }

        DiggerActionType ActionType { get; set; }

        string ResultFilePath { get; set; } 
    }
}
