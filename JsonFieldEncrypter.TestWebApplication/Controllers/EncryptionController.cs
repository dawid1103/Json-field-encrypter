using JsonFieldEncrypter.Tools;
using Microsoft.AspNetCore.Mvc;

namespace JsonFieldEncrypter.TestWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncryptionController : ControllerBase
    {
        private readonly IEncryptionService encryptionService;

        public EncryptionController(IEncryptionService encryptionService)
        {
            this.encryptionService = encryptionService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string encrypted = encryptionService.Encrypt("Text");
            string decrypted = encryptionService.Decrypt(encrypted);
            
            return Ok();
        }
    }
}