using Digger.Core.Handlers;
using NUnit.Framework;

namespace Digger.Tests.Handlers
{
    class CsHandlerTest : BaseTestClass
    {
        private CsHandler _csHandler;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _csHandler = new CsHandler(_diggerConfigurationMock.Object, _fileServiceMock.Object);
        }

        [TestCase(@"C:\folder1\folder2\file1.txt", @"\folder1\folder2\file1.txt", "C:")]
        [TestCase(@"C:\folder1\folder2\file1.txt", @"\file1.txt", @"C:\folder1\folder2")]
        [TestCase(@"C:\folder1\folder2\file1.txt", @"\folder2\file1.txt", @"C:\folder1")]
        public void TransformFile_FilePathValid_ReturnCorrectValue(string filePath, string filePathTransformed, string rootFolder)
        {
            _diggerConfigurationMock.Setup(x => x.RootFolder).Returns(rootFolder);

            var result = _csHandler.TransformFile(filePath);

            Assert.AreEqual(filePathTransformed, result);
        }
    }
}
