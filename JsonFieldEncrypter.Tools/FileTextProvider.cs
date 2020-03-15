using System.IO;

namespace JsonFieldEncrypter.Tools
{
    public interface IFileTextProvider
    {
        string GetText(string filePath);
    }
    
    internal class FileTextProvider : IFileTextProvider
    {
        public string GetText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}