using Infrastructure.Context;
using Luxury.Api.Application.Commands;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Luxury.Api.Test.Application.Commands
{
    [TestFixture]
    public class CreatePropertyTraceCommandHandlerTest
    {
        private DbContextOptions<MillionDbContext> _dbContextOptions;
        private MillionDbContext _context;
        private CreatePropertyTraceCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            _context = new MillionDbContext(_dbContextOptions);


            _handler = new CreatePropertyTraceCommandHandler(_context);
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
            var command = new CreatePropertyTraceCommand
            (
                DateTime.UtcNow,
                "Test Trace",
                5000,
                150,
                1
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Correct PropertyTrace creation"));
            Assert.That(result.Data, Is.Not.Null);

            var propertyTrace = await _context.PropertyTrace.FindAsync(result.Data);
            Assert.That(propertyTrace, Is.Not.Null);
            Assert.That(propertyTrace.Name, Is.EqualTo(command.Name));
            Assert.That(propertyTrace.Value, Is.EqualTo(command.Value));
            Assert.That(propertyTrace.Tax, Is.EqualTo(command.Tax));
            Assert.That(propertyTrace.IdProperty, Is.EqualTo(command.IdProperty));
        }

        [Test]
        public async Task Handle_ExceptionThrown_ReturnsFailureResponse()
        {
            // Arrange
            var mockContext = new Mock<MillionDbContext>(_dbContextOptions);
            mockContext.Setup(ctx => ctx.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var handler = new CreatePropertyTraceCommandHandler(mockContext.Object);

            var command = new CreatePropertyTraceCommand
            (
                DateTime.UtcNow,
                "Test Trace",
                5000,
                150,
                1
            );

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.False);
        }
    }
}
