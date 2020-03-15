using Microsoft.Extensions.Options;

namespace JsonFieldEncrypter.Tools
{
    public interface IEncryptionService
    {
        string Encrypt(string textToEncrypt);
        string Decrypt(string encryptedText);
    }

    internal class EncryptionService : IEncryptionService
    {
        private readonly IFileTextProvider textProvider;
        private readonly EncryptionServiceOptions options;

        public EncryptionService(IOptions<EncryptionServiceOptions> options, IFileTextProvider textProvider)
        {
            this.textProvider = textProvider;
            this.options = options.Value;
        }

        private string GetKey() => textProvider.GetText(options.EncryptionKeyFilePath);

        public string Encrypt(string textToEncrypt)
        {
            return Encrypter.Encrypt(textToEncrypt, GetKey());
        }

        public string Decrypt(string encryptedText)
        {
            return Encrypter.Decrypt(encryptedText, GetKey());
        }
    }
}