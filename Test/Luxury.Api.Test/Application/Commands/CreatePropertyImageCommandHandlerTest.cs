using Infrastructure.Context;
using Luxury.Api.Application.Commands;
using Luxury.Api.Application.Managers.Property;
using Luxury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Luxury.Api.Test.Application.Commands
{
    [TestFixture]
    public class CreatePropertyImageCommandHandlerTest
    {
        private Mock<IPropertyManager> _mockPropertyManager;
        private DbContextOptions<MillionDbContext> _dbContextOptions;
        private MillionDbContext _context;
        private CreatePropertyImageCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            _context = new MillionDbContext(_dbContextOptions);

            _mockPropertyManager = new Mock<IPropertyManager>();
            _mockPropertyManager.Setup(pm => pm.getByteImage(It.IsAny<string>()))
                .Returns<string>(img => Convert.FromBase64String(img));

            _handler = new CreatePropertyImageCommandHandler(_context, _mockPropertyManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new CreatePropertyImageCommand
            (
                Convert.ToBase64String(new byte[] { 1, 2, 3 }),
                true,
                1
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Correct PropertyImage creation"));
            Assert.That(result.Data, Is.InstanceOf<PropertyImage>());

            var image = result.Data as PropertyImage;
            Assert.That(image, Is.Not.Null);
            Assert.That(image.FileImage, Is.EqualTo(Convert.FromBase64String(command.FileImage)));
            Assert.That(image.Enable, Is.EqualTo(command.Enable));
            Assert.That(image.IdProperty, Is.EqualTo(command.IdProperty));
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            _mockPropertyManager.Setup(pm => pm.getByteImage(It.IsAny<string>()))
                .Throws(new Exception("Test exception"));

            var command = new CreatePropertyImageCommand
            (
                "invalid_base64",
                true,
                1
            );


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Does.Contain("Test exception"));
        }
    }
}
