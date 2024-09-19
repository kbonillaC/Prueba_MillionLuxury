using Luxury.Api.Application.Managers.Jwt;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Luxury.Api.Test.Application.Managers
{
    [TestFixture]
    public class JwtManagerTest
    {
        private JwtManager _jwtManager;
        private Mock<IConfiguration> _mockConfig;

        [SetUp]
        public void SetUp()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(config => config["Jwt:Key"]).Returns("GlCWcmX+eHsPx6b0SjhCGdV7ikwfH40YqXlgb66v4og=");
            _mockConfig.Setup(config => config["Jwt:Issuer"]).Returns("https://your-issuer");
            _mockConfig.Setup(config => config["Jwt:Audience"]).Returns("https://your-audience");

            _jwtManager = new JwtManager(_mockConfig.Object);
        }

        [Test]
        public void GetToken_ReturnsValidToken()
        {
            // Act
            var token = _jwtManager.GetToken();

            // Assert
            Assert.That(token, Is.Not.Null.Or.Empty);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.That(jwtToken, Is.Not.Null);
            Assert.That(jwtToken.Issuer, Is.EqualTo("https://your-issuer"));
            Assert.That(jwtToken.Audiences, Contains.Item("https://your-audience"));

            var claims = jwtToken.Claims;
            var claim = claims.FirstOrDefault(c => c.Type == "JWT_LuxuryAPI");

            Assert.That(claim, Is.Not.Null);
            Assert.That(claim.Value, Is.EqualTo("Luxury"));
        }

        [Test]
        public void GetToken_ShouldHaveCorrectClaims()
        {
            // Act
            var token = _jwtManager.GetToken();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Assert
            var claims = jwtToken.Claims;
            var claim = claims.FirstOrDefault(c => c.Type == "JWT_LuxuryAPI");

            Assert.That(claim, Is.Not.Null);
            Assert.That(claim.Value, Is.EqualTo("Luxury"));
        }
    }
}
