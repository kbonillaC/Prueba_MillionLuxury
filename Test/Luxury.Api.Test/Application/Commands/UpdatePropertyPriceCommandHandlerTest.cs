using Infrastructure.Context;
using Luxury.Api.Application.Commands;
using Luxury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Luxury.Api.Test.Application.Commands
{
    public class UpdatePropertyPriceCommandHandlerTest
    {
        private DbContextOptions<MillionDbContext> _dbContextOptions;
        private MillionDbContext _context;
        private UpdatePropertyPriceCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            _context = new MillionDbContext(_dbContextOptions);
            _handler = new UpdatePropertyPriceCommandHandler(_context);
            _context.Property.AddRange(
               new Property { IdProperty = 5, Address = "123 kr", Name = "Property1", Price = 1000 },
               new Property { IdProperty = 1, Address = "456 kr", Name = "Property2", Price = 2000 }
           );
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context?.Dispose();
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new UpdatePropertyPriceCommand
            (
                1,
                2000
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Property price updated successfully"));

            var property = await _context.Property.FindAsync(command.PropertyId);
            Assert.That(property, Is.Not.Null);
            Assert.That(property.Price, Is.EqualTo(command.Price));
        }

        [Test]
        public async Task Handle_PropertyNotFound_ReturnsFailureResponse()
        {
            // Arrange
            var command = new UpdatePropertyPriceCommand
            (
                999,
                3000
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Property not found"));
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var mockContext = new Mock<MillionDbContext>(_dbContextOptions);
            mockContext.Setup(ctx => ctx.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var handler = new UpdatePropertyPriceCommandHandler(mockContext.Object);

            var command = new UpdatePropertyPriceCommand
            (
                1,
                2000
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.False);
        }
    }
}
