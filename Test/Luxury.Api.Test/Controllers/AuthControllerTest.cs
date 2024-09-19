using Luxury.Api.Application.Managers.Jwt;
using Luxury.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Luxury.Api.Test.Controllers
{
    [TestFixture]
    public class AuthControllerTest
    {
        private Mock<IJwtManager> _jwtManagerMock;
        private AuthController _controller;

        [SetUp]
        public void SetUp()
        {
            _jwtManagerMock = new Mock<IJwtManager>();
            _controller = new AuthController(_jwtManagerMock.Object);
        }

        [Test]
        public void Get_ReturnsOkResultWithToken()
        {
            // Arrange
            var expectedToken = "mocked-jwt-token";
            _jwtManagerMock.Setup(j => j.GetToken()).Returns(expectedToken);

            // Act
            var result = _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);

            Assert.That(200, Is.EqualTo(okResult.StatusCode));

        }
    }
}
