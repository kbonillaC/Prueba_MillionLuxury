using Infrastructure.Context;
using Luxury.Api.Application.Querys;
using Luxury.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Luxury.Api.Test.Application.Querys
{
    [TestFixture]
    public class GetAllQueryHandlerTest
    {
        private MillionDbContext _context;
        private GetAllQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<MillionDbContext>()
                 .UseInMemoryDatabase(databaseName: databaseName)
                 .Options;

            _context = new MillionDbContext(options);
            _handler = new GetAllQueryHandler(_context);

            _context.Property.AddRange(
                new Property { IdProperty = 1, Address = "123 kr", Name = "Property1", Price = 1000 },
                new Property { IdProperty = 2, Address = "456 kr", Name = "Property2", Price = 2000 }
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
        public async Task Handle_WhenDataExists_ReturnsSuccessResponse()
        {
            // Arrange
            var query = new GetAllQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("data found"));
            Assert.That(result.Data, Is.Not.Null);
        }

    }
}
