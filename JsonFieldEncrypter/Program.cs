using System;
using System.IO;
using JsonFieldEncrypter.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonFieldEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("JsonFieldEncrypter.exe appsettings.json key/file.txt foo.bar.password");
            }

            string file = args[0];
            string text = File.ReadAllText(file);
            string encriptionKey = File.ReadAllText(args[1]);
            string jsonPath = args[2];

            JObject obj = JObject.Parse(text);
            JToken valueToken = obj.SelectToken(jsonPath);

            string value = valueToken.Value<string>();

            string encryptedValue = Encrypter.Encrypt(value, encriptionKey);

            valueToken.Replace(encryptedValue);
            File.WriteAllText(file, obj.ToString(Formatting.Indented));
        }
    }
}