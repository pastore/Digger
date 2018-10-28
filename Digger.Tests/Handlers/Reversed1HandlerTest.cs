using Digger.Core.Handlers;
using NUnit.Framework;

namespace Digger.Tests.Handlers
{
    [TestFixture]
    class Reversed1HandlerTest : BaseTestClass
    {
        private Reversed1Handler _reversed1Handler;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _reversed1Handler = new Reversed1Handler(_diggerConfigurationMock.Object, _fileServiceMock.Object);
        }

        [TestCase(@"C:\folder1\folder2\file1.txt", @"file1.txt\folder2\folder1\", "C:")]
        [TestCase(@"C:\folder1\folder2\file1.txt", @"file1.txt\", @"C:\folder1\folder2")]
        [TestCase(@"C:\folder1\folder2\file1.txt", @"file1.txt\folder2\", @"C:\folder1")]
        public void TransformFile_FilePathValid_ReturnCorrectValue(string filePath, string filePathTransformed, string rootFolder)
        {
            _diggerConfigurationMock.Setup(x => x.RootFolder).Returns(rootFolder);

            var result = _reversed1Handler.TransformFile(filePath);

            Assert.AreEqual(filePathTransformed, result);
        }
    }
}
