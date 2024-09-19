using Infrastructure.Context;
using Luxury.Api.Application.Commands;
using Microsoft.EntityFrameworkCore;

namespace Luxury.Api.Test.Application.Commands
{


    [TestFixture]
    public class CreatePropertyCommandHandlerTest
    {
        private DbContextOptions<MillionDbContext> _dbContextOptions;
        private MillionDbContext _context;
        private CreatePropertyCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            _context = new MillionDbContext(_dbContextOptions);



            _handler = new CreatePropertyCommandHandler(_context);
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
            var command = new CreatePropertyCommand
            (
                "Property Name",
                "456 kr",
                1000,
                123,
                2024,
                1
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Correct Property creation"));
            Assert.That(result.Data, Is.Not.Null);

            var property = await _context.Property.FindAsync(result.Data);
            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo(command.Name));
            Assert.That(property.Address, Is.EqualTo(command.Address));
            Assert.That(property.Price, Is.EqualTo(command.Price));
            Assert.That(property.CodeInternal, Is.EqualTo(command.CodeInternal));
            Assert.That(property.Year, Is.EqualTo(command.Year));
            Assert.That(property.IdOwner, Is.EqualTo(command.IdOwner));
        }

    }
}
