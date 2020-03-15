using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace JsonFieldEncrypter.Tools.Tests
{
    public class EncryptionServiceTests
    {
        private const string EncryptionKey = "JDH37FGNSNB93GKDJ38TJF&#;/)(JSIT";
        private readonly Mock<IFileTextProvider> fileTextProvider;
        private readonly Mock<IOptions<EncryptionServiceOptions>> options;
        private readonly EncryptionServiceOptions optionsObj;

        public EncryptionServiceTests()
        {
            optionsObj = new EncryptionServiceOptions
            {
                EncryptionKeyFilePath = "file.txt"
            };

            fileTextProvider = new Mock<IFileTextProvider>();
            fileTextProvider.Setup(x => x.GetText(optionsObj.EncryptionKeyFilePath))
                .Returns(EncryptionKey);

            options = new Mock<IOptions<EncryptionServiceOptions>>();
            options.Setup(x => x.Value).Returns(optionsObj);
        }

        [Fact]
        public void Encrypt_ShouldGetKeyFileTextFromProvider()
        {
            EncryptionService service = new EncryptionService(options.Object, fileTextProvider.Object);
            service.Encrypt(It.IsAny<string>());

            fileTextProvider.Verify(x => x.GetText(optionsObj.EncryptionKeyFilePath));
        }

        [Fact]
        public void EncryptDecrypt_ShouldReturnSameString()
        {
            const string expected = "Lorem ipsum!@#$%^&*()_+{:>?<><.,/ąśćółęźćż";
            EncryptionService service = new EncryptionService(options.Object, fileTextProvider.Object);
            string encrypted = service.Encrypt(expected);
            string actual = service.Decrypt(encrypted);

            Assert.Equal(expected, actual);
        }
    }
}