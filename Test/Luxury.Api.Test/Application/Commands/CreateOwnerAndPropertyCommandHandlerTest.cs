using Infrastructure.Context;
using Luxury.Api.Application.Commands;
using Luxury.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Luxury.Api.Test.Application.Commands
{
    [TestFixture]
    public class CreateOwnerAndPropertyCommandHandlerTest
    {
        private DbContextOptions<MillionDbContext> _dbContextOptions;
        private MillionDbContext _context;
        private Mock<IMediator> _mediatorMock;
        private CreateOwnerAndPropertyCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            _dbContextOptions = new DbContextOptionsBuilder<MillionDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            _context = new MillionDbContext(_dbContextOptions);

            _mediatorMock = new Mock<IMediator>();

            _handler = new CreateOwnerAndPropertyCommandHandler(_context, _mediatorMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Handle_WhenAllCommandsSucceed_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new CreateOwnerAndPropertyCommand
            (
                "Algun nombre",
                "123 kr",
                "photoBase64",
                DateTime.UtcNow,
                "Property Name",
                "456 kr",
                1000,
                123,
                2024,
                "asdw",
                true
            );

            var ownerResponse = new LuxuryResponse
            {
                Success = true,
                Message = "Correct owner creation",
                Data = 1
            };

            var propertyResponse = new LuxuryResponse
            {
                Success = true,
                Message = "Correct Property creation",
                Data = 2
            };

            var propertyImageResponse = new LuxuryResponse
            {
                Success = true,
                Message = "Correct PropertyImage creation",
                Data = null
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOwnerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ownerResponse);

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePropertyCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(propertyResponse);

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePropertyImageCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(propertyImageResponse);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Correct PropertyImage creation"));
        }
    }
}
