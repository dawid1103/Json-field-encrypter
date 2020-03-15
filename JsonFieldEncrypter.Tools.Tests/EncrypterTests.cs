using Xunit;

namespace JsonFieldEncrypter.Tools.Tests
{
    public class UnitTest1
    {
        private const string encryptionKey = "JDH37FGNSNB93GKDJ38TJF&#;/)(JSIT";

        private const string longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut " +
                                          "labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                                          "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse " +
                                          "cillum dolore eu fugiat nulla pariatur.";

        private const string shortString = "Lorem ipsum";
        private const string specialChars = "Lorem ipsum!@#$%^&*()_+{:>?<><.,/ąśćółęźćż";


        [Theory]
        [InlineData(longString)]
        [InlineData(shortString)]
        [InlineData(specialChars)]
        public void EncryptDecrypt_LongString_ShouldReturnSameString(string expected)
        {
            string encrypted = Encrypter.Encrypt(expected, encryptionKey);
            string actual = Encrypter.Decrypt(encrypted, encryptionKey);

            Assert.Equal(expected, actual);
        }
    }
}