using Digger.Core.Interfaces;
using Moq;

namespace Digger.Tests.Handlers
{
    public class BaseTestClass
    {
        protected Mock<IDiggerConfiguration> _diggerConfigurationMock;
        protected Mock<IFileService> _fileServiceMock;

        public virtual void Init()
        {
            _diggerConfigurationMock = new Mock<IDiggerConfiguration>();
            _fileServiceMock = new Mock<IFileService>();
        }
    }
}
