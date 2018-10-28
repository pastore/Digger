using Digger.Core.Handlers;
using NUnit.Framework;

namespace Digger.Tests.Handlers
{
    [TestFixture]
    class Reversed2HandlerTest : BaseTestClass
    {
        private Reversed2Handler _reversed2Handler;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _reversed2Handler = new Reversed2Handler(_diggerConfigurationMock.Object, _fileServiceMock.Object);
        }

        [TestCase(@"C:\folder1\folder2\file1.txt", @"txt.1elif\2redlof\1redlof\", "C:")]
        [TestCase(@"C:\folder1\folder2\file1.txt", @"txt.1elif\", @"C:\folder1\folder2")]
        [TestCase(@"C:\folder1\folder2\file1.txt", @"txt.1elif\2redlof\", @"C:\folder1")]
        public void TransformFile_FilePathValid_ReturnCorrectValue(string filePath, string filePathTransformed, string rootFolder)
        {
            _diggerConfigurationMock.Setup(x => x.RootFolder).Returns(rootFolder);

            var result = _reversed2Handler.TransformFile(filePath);

            Assert.AreEqual(filePathTransformed, result);
        }
    }
}
