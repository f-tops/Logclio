using Logclio.Api.Controllers;
using Logclio.Domain.Repositories.LogEntry;
using Logclio.Domain.Services.LogFileQueueService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Logclio.Test.Api.Controllers
{
    public class LogsControllerTests
    {
        private readonly Mock<ILogFileQueueService> _mockLogFileQueueService;
        private readonly Mock<ILogEntryRepository> _mockLogEntryRepository;
        private readonly LogsController _controller;

        public LogsControllerTests()
        {
            _mockLogFileQueueService = new Mock<ILogFileQueueService>();
            _mockLogEntryRepository = new Mock<ILogEntryRepository>();
            _controller = new LogsController(_mockLogFileQueueService.Object, _mockLogEntryRepository.Object);
        }

        [Fact]
        public async Task UploadAsync_ShouldReturnOkResult_WhenFileIsValid()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var content = "2020-11-04 11:05:00.370 +01:00 [INF] {E2C67223-862D-40D5-B85E-47D75BF7C4A3} [Extended document processing] Executed AddWatermarkStateAction on object (0-4649644).";
            var fileName = "logfile.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            // Act
            var result = await _controller.UploadAsync(fileMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("File uploaded successfully.", okResult.Value);
        }

        [Fact]
        public async Task UploadAsync_ShouldReturnBadRequest_WhenFileIsNull()
        {
            // Act
            var result = await _controller.UploadAsync(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", badRequestResult.Value);
        }

        [Fact]
        public async Task UploadAsync_ShouldReturnBadRequest_WhenFileExtensionIsNotSupported()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var content = "2020-11-04 11:05:00.370 +01:00 [INF] {E2C67223-862D-40D5-B85E-47D75BF7C4A3} [Extended document processing] Executed AddWatermarkStateAction on object (0-4649644).";
            var fileName = "logfile.unsupported";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            // Act
            var result = await _controller.UploadAsync(fileMock.Object);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Unsupported file extension!", badRequestResult.Value);
        }
    }
}
