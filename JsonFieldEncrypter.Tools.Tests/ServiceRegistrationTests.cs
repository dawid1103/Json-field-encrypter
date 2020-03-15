using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace JsonFieldEncrypter.Tools.Tests
{
    public class ServiceRegistrationTests
    {
        private readonly Mock<IConfiguration> configuration;

        public ServiceRegistrationTests()
        {
            configuration = new Mock<IConfiguration>();
            configuration
                .Setup(x =>
                    x.GetSection(nameof(EncryptionServiceOptions)))
                .Returns(It.IsAny<IConfigurationSection>());
        }

        [Fact]
        public void AddEncryption_ShouldRegisterServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddEncryption(configuration.Object);

            var encryptionService = serviceCollection.First(x => 
                x.ServiceType == typeof(IEncryptionService));
            
            var options = serviceCollection.First(x => 
                x.ServiceType == typeof(IConfigureOptions<EncryptionServiceOptions>));
        }
    }
}