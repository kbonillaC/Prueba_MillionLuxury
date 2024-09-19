using Infrastructure.Context;
using Luxury.Api.Application.Commands;
using Luxury.Api.Application.Managers.Property;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Luxury.Api.Test.Application.Commands
{
    [TestFixture]
    public class CreateOwnerCommandHandlerTest
    {
        private DbContextOptions<MillionDbContext> _dbContextOptions;
        private MillionDbContext _context;
        private Mock<IPropertyManager> _propertyManagerMock;
        private CreateOwnerCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            _context = new MillionDbContext(_dbContextOptions);

            _propertyManagerMock = new Mock<IPropertyManager>();

            _handler = new CreateOwnerCommandHandler(_context, _propertyManagerMock.Object);
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
            var command = new CreateOwnerCommand
            (
                "Algun Nombre",
                "123 kr",
                "photoBase64",
                DateTime.UtcNow
            );

            var photoBytes = new byte[] { 1, 2, 3 };
            _propertyManagerMock.Setup(pm => pm.getByteImage(It.IsAny<string>()))
                .Returns(photoBytes);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Correct owner creation"));
            Assert.That(result.Data, Is.Not.Null);

            var owner = await _context.Owners.FindAsync(result.Data);
            Assert.That(owner, Is.Not.Null);
            Assert.That(owner.Name, Is.EqualTo(command.Name));
            Assert.That(owner.Address, Is.EqualTo(command.Address));
            Assert.That(owner.Photo, Is.EqualTo(photoBytes));
            Assert.That(owner.Birthday, Is.EqualTo(command.Birthday));
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var command = new CreateOwnerCommand
            (
                "Algun nombre",
                "123 kr",
                "photoBase64",
                DateTime.UtcNow
            );

            _propertyManagerMock.Setup(pm => pm.getByteImage(It.IsAny<string>()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Does.Contain("Test exception"));
        }
    }
}
