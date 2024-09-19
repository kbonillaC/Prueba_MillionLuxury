using Luxury.Api.Application.Managers.Property;

namespace Luxury.Api.Test.Application.Managers
{
    [TestFixture]
    public class PropertyManagerTest
    {
        private PropertyManager _propertyManager;

        [SetUp]
        public void SetUp()
        {
            _propertyManager = new PropertyManager();
        }

        [Test]
        public void GetByteImage_ValidBase64Image_ReturnsByteArray()
        {
            // Arrange
            string base64Image = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDCAwAAACmJLR0QAAAAAAAD6AABQeF3ZAAAACXBIWXMAAB7CAAAewgFu0HU+AAABeElEQVQ4T2NkYGBg8AAwABr71AdGUC4XKAlqgK7CTo2IwPBAgA8YAA8kYIpXkAYIZINlzwOD/I5gDwR1CDxBiTg5ihHRZC0A5wHYMxkQEvmbxhAMWlB2gI0M1kEqAogABMD7dA4xCTYZQNBWAVUD/xC5glFq+VoBlMxFBMThRV0UpoG3kkcAULzJQMcAQC9UAC6gQYACfARAVhKOM2mW4jIxMCwLBwP2xHHFPhcOB8GCJ1qKAxPQjoQsUAFASZ9QPdO5CAYRpHDWMdZKLUADSPJLD/4uQAaHLKKUkC5HCBuDEYZArK/QaAIGJ2HECDvHroN/AoAAAEBAIEABzMAB5wAAAAAElFTkSuQmCC";

            // Act
            var result = _propertyManager.getByteImage(base64Image);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.GreaterThan(0));
        }

        [Test]
        public void GetByteImage_InvalidBase64Image_ThrowsFormatException()
        {
            // Arrange
            string invalidBase64Image = "data:image/png;base64,invalid_base64";

            // Act & Assert
            Assert.Throws<FormatException>(() => _propertyManager.getByteImage(invalidBase64Image));
        }


    }
}
