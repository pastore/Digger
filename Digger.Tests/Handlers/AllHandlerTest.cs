using Digger.Core.Enums;
using Digger.Core.Handlers;
using Digger.Core.Utils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Digger.Tests.Handlers
{
    [TestFixture]
    public class AllHandlerTest : BaseTestClass
    {
        private AllHandler _allHandler;

        [SetUp]
        public override void Init() 
        {
            base.Init();
            _diggerConfigurationMock.Setup(x => x.RootFolder).Returns(Constants.DefaultRootFolder);
            _diggerConfigurationMock.Setup(x => x.ResultFilePath).Returns(Constants.DefaultResultPath);
            _diggerConfigurationMock.Setup(x => x.ActionType).Returns(DiggerActionType.All);
            _allHandler = new AllHandler(_diggerConfigurationMock.Object, _fileServiceMock.Object);
        }

        [Test]
        public async Task HandleAsync_TasksCompleted_Success()
        {
            _fileServiceMock.Setup(x => x.DigFolderAsync(It.IsAny<DirectoryInfo>(), It.IsAny<string>(),
                It.IsAny<Func<string, string>>(), It.IsAny<ConcurrentQueue<string>>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            _fileServiceMock.Setup(x =>
                x.WriteFilesToFileAsync(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            await _allHandler.HandleAsync(new CancellationToken());

            _fileServiceMock.Verify(x => x.DigFolderAsync(It.IsAny<DirectoryInfo>(), It.IsAny<string>(),
                It.IsAny<Func<string, string>>(), It.IsAny<ConcurrentQueue<string>>(), It.IsAny<CancellationToken>()), Times.Once);

            _fileServiceMock.Verify(x =>
                x.WriteFilesToFileAsync(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void HandleAsync_ThrowException_Fail()
        {
            _fileServiceMock.Setup(x => x.DigFolderAsync(It.IsAny<DirectoryInfo>(), It.IsAny<string>(),
                It.IsAny<Func<string, string>>(), It.IsAny<ConcurrentQueue<string>>(), It.IsAny<CancellationToken>())).ThrowsAsync(new AggregateException());

            Assert.ThrowsAsync<AggregateException>(async () =>
            {
                await _allHandler.HandleAsync(new CancellationToken());
            });
        }

        [Test]
        public void TransformFile_FilePathIsNull_ThrowException()
        {
            Assert.Throws<NullReferenceException>(() => { _allHandler.TransformFile(null); });
        }
    }
}
