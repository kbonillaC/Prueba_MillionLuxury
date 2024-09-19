using Infrastructure.Context;
using Luxury.Api.Application.Querys;
using Luxury.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luxury.Api.Test.Application.Querys
{
    [TestFixture]
    public class GetAllPropetiesByNameQueryHandlerTest
    {
        private MillionDbContext _context;
        private GetAllPropetiesByNameQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            var databaseName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<MillionDbContext>()
                 .UseInMemoryDatabase(databaseName: databaseName)
                 .Options;

            _context = new MillionDbContext(options);
            _handler = new GetAllPropetiesByNameQueryHandler(_context);

            _context.Property.AddRange(
                new Property { IdProperty = 5, Address = "123 kr", Name = "Property1", Price = 1000 },
                new Property { IdProperty = 6, Address = "456 kr", Name = "Property2", Price = 2000 }
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
        public async Task Handle_WhenPropertiesFound_ReturnsSuccessResponse()
        {
            // Arrange
            var query = new GetAllPropetiesByNameQuery("Property1");

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("data found"));
            Assert.That(result.Data, Is.Not.Null);
        }

        [Test]
        public async Task Handle_WhenNoPropertiesFound_ReturnsFailureResponse()
        {
            // Arrange
            var query = new GetAllPropetiesByNameQuery( "NonExistentProperty");

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("No data found"));
            Assert.That(result.Data, Is.Null);
        }
    }
}
