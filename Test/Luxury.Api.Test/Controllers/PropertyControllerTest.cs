using Luxury.Api.Application.Commands;
using Luxury.Api.Application.Querys;
using Luxury.Api.Controllers;
using Luxury.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Luxury.Api.Test.Controllers
{
    [TestFixture]
    public class PropertyControllerTest
    {
        private Mock<IMediator> _mediator;
        private PropertyController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediator = new();
            _controller = new PropertyController(_mediator.Object);
        }

        [Test]
        public async Task Get_ReturnsOkResult()
        {

            // Arrange;
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new List<Property> { new Property { Address = "alguna dir", CodeInternal = 1, IdOwner = 1, IdProperty = 1, Name = "algun nombre real", Price = 1000, Year = 2024 } }
            };
            _mediator.Setup(j => j.Send(It.IsAny<GetAllQuery>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");

        }

        [Test]
        public async Task GetPropertiesByName_ReturnsOkResult()
        {

            // Arrange;
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new List<Property> { new Property { Address = "alguna dir", CodeInternal = 1, IdOwner = 1, IdProperty = 1, Name = "algun nombre real", Price = 1000, Year = 2024 } }
            };
            _mediator.Setup(j => j.Send(It.IsAny<GetAllPropetiesByNameQuery>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetPropertiesByName(It.IsAny<string>());

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task CreateOwner_ReturnsOkResult()
        {
            // Arrange;
            var createOwnerCommand = new CreateOwnerCommand("algun nombre", "alguna dir", "alguna_imagen_base64", DateTime.UtcNow);
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new List<Property> { new Property { Address = "alguna dir", CodeInternal = 1, IdOwner = 1, IdProperty = 1, Name = "algun nombre real", Price = 1000, Year = 2024 } }
            };
            _mediator.Setup(j => j.Send(It.IsAny<CreateOwnerCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateOwner(createOwnerCommand);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task CreateOwner_ReturnsBadRequest()
        {
            // Arrange;
            var createOwnerCommand = new CreateOwnerCommand("", "alguna dir", "alguna_imagen_base64", DateTime.UtcNow);

            // Act
            var result = await _controller.CreateOwner(createOwnerCommand);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be BadRequestObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }

        [Test]
        public async Task CreateProperty_ReturnsOkResult()
        {
            // Arrange;
            var createPropertyCommand = new CreatePropertyCommand("algun nombre", "alguna dir", 100, 1, 2024, 1);
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new Property { Address = "alguna dir", CodeInternal = 1, IdOwner = 1, IdProperty = 1, Name = "algun nombre real", Price = 1000, Year = 2024 }
            };
            _mediator.Setup(j => j.Send(It.IsAny<CreatePropertyCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateProperty(createPropertyCommand);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task CreateProperty_ReturnsBadRequest()
        {
            // Arrange;
            var createPropertyCommand = new CreatePropertyCommand("algun nombre", "alguna dir", 100, 1, 2024, 0);

            // Act
            var result = await _controller.CreateProperty(createPropertyCommand);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }

        [Test]
        public async Task CreatePropertyImage_ReturnsOkResult()
        {
            // Arrange;
            var createPropertyImageCommand = new CreatePropertyImageCommand("Alguna Imagen base64",true,12);
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new PropertyImage { Enable = true, IdProperty = 1, IdPropertyImage = 1}
            };
            _mediator.Setup(j => j.Send(It.IsAny<CreatePropertyImageCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreatePropertyImage(createPropertyImageCommand);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task CreatePropertyImage_ReturnsBadRequest()
        {
            // Arrange;
            var createPropertyImageCommand = new CreatePropertyImageCommand("Alguna Imagen base64", true, 0);

            // Act
            var result = await _controller.CreatePropertyImage(createPropertyImageCommand);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }

        [Test]
        public async Task CreateOwnerAndProperty_ReturnsOkResult()
        {
            // Arrange;
            var createOwnerAndPropertyCommand = new CreateOwnerAndPropertyCommand(
                "algun nombre", "alguna dir", "alguna_imagen_base64", DateTime.UtcNow,"algun nombre de propiedad","alguna dir propiedad",123,31,2024,"alguna base64",true
                );
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new PropertyImage { Enable = true, IdProperty = 1, IdPropertyImage = 1 }
            };
            _mediator.Setup(j => j.Send(It.IsAny<CreateOwnerAndPropertyCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreateOwnerAndProperty(createOwnerAndPropertyCommand);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task CreateOwnerAndProperty_ReturnsBadRequest()
        {
            // Arrange;
            var createOwnerAndPropertyCommand = new CreateOwnerAndPropertyCommand(
               "", "alguna dir", "alguna_imagen_base64", DateTime.UtcNow, "algun nombre de propiedad", "alguna dir propiedad", 123, 31, 2024, "alguna base64", true
               );

            // Act
            var result = await _controller.CreateOwnerAndProperty(createOwnerAndPropertyCommand);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }

        [Test]
        public async Task CreatePropertyTrace_ReturnsOkResult()
        {
            // Arrange;
            var createPropertyTraceCommand = new CreatePropertyTraceCommand(DateTime.UtcNow,"nombre",123,23.8M,1);
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = 3
            };
            _mediator.Setup(j => j.Send(It.IsAny<CreatePropertyTraceCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.CreatePropertyTrace(createPropertyTraceCommand);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task CreatePropertyTrace_ReturnsBadRequest()
        {
            // Arrange;
            var createPropertyTraceCommand = new CreatePropertyTraceCommand(DateTime.UtcNow, "nombre", 123, 23.8M, 0);

            // Act
            var result = await _controller.CreatePropertyTrace(createPropertyTraceCommand);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }
        
        [Test]
        public async Task UpdatePrice_ReturnsOkResult()
        {
            // Arrange;
            int propertyId = 1;
            long newPrice = 1000;
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new Property { Address = "alguna dir", CodeInternal = 1, IdOwner = 1, IdProperty = 1, Name = "algun nombre real", Price = 1000, Year = 2024 }
            };
            _mediator.Setup(j => j.Send(It.IsAny<UpdatePropertyPriceCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UpdatePrice(propertyId, newPrice);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task UpdatePrice_ReturnsBadRequest()
        {
            // Act
            var result = await _controller.UpdatePrice(It.IsAny<int>(), It.IsAny<long>());

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }
        //
        [Test]
        public async Task UpdateProperty_ReturnsOkResult()
        {
            // Arrange;
            int propertyId = 1;
            var updatePropertyCommand = new UpdatePropertyCommand(1,"algun Nombre","Alguna dir",122,3,2024,1);
            LuxuryResponse expectedResponse = new LuxuryResponse
            {
                Success = true,
                Message = "data found",
                Data = new Property { Address = "alguna dir", CodeInternal = 1, IdOwner = 1, IdProperty = 1, Name = "algun nombre real", Price = 1000, Year = 2024 }
            };
            _mediator.Setup(j => j.Send(It.IsAny<UpdatePropertyCommand>(), default))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.UpdateProperty(propertyId, updatePropertyCommand);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(okResult.StatusCode, Is.EqualTo(200), "Status code should be 200");

            var response = okResult.Value as LuxuryResponse;
            Assert.That(response, Is.Not.Null, "Response should not be null");
            Assert.That(response.Success, Is.True, "Response should indicate success");
            Assert.That(response.Data, Is.Not.Null, "Response data should not be null");
        }

        [Test]
        public async Task UpdateProperty_ReturnsBadRequest()
        {
            //Arrange
            var updatePropertyCommand = new UpdatePropertyCommand(1, "algun Nombre", "Alguna dir", 122, 3, 2024, 1);
            // Act
            var result = await _controller.UpdateProperty(It.IsAny<int>(), updatePropertyCommand);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.That(badRequestResult, Is.Not.Null, "Result should be OkObjectResult");
            Assert.That(badRequestResult.StatusCode, Is.EqualTo(400), "Status code should be 400");
        }
    }
}
