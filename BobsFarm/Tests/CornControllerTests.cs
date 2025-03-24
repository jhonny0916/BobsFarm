using AutoMapper;
using BobsFarm.Controllers;
using BobsFarm.Models.Error;
using BobsFarm_AL.Interfaces;
using BobsFarm_BO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BobsFarm.Tests
{
    public class CornControllerTests
    {
        private readonly Mock<ICornService> _cornServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CornController _controller;
        public CornControllerTests()
        {
            _cornServiceMock = new Mock<ICornService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new CornController(_cornServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task BuyCorn_Success_ReturnsOk()
        {
            _cornServiceMock.Setup(s => s.BuyCorn(It.IsAny<string>()));

            var result = await _controller.BuyCorn("testClient");

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task BuyCorn_TooManyRequests_Returns429()
        {
            _cornServiceMock.Setup(s => s.BuyCorn(It.IsAny<string>()));

            var result = await _controller.BuyCorn("testClient");

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(429, statusCodeResult.StatusCode);
            Assert.Equal("Too Many Requests", statusCodeResult.Value);
        }

        [Fact]
        public async Task BuyCorn_BLException_ReturnsBadRequest()
        {
            var blException = new BLException(new BOError { Message = "Invalid client" });
            _cornServiceMock.Setup(s => s.BuyCorn(It.IsAny<string>())).ThrowsAsync(blException);
            _mapperMock.Setup(m => m.Map<BOError, ErrorResponse>(blException._error))
                       .Returns(new ErrorResponse { Message = "Invalid client" });

            var result = await _controller.BuyCorn("testClient");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
            Assert.Equal("Invalid client", errorResponse.Message);
        }

        [Fact]
        public async Task BuyCorn_Exception_Returns500()
        {
            _cornServiceMock.Setup(s => s.BuyCorn(It.IsAny<string>())).ThrowsAsync(new Exception("Internal error"));

            var result = await _controller.BuyCorn("testClient");

            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Internal error", statusCodeResult.Value);
        }
    }
}
