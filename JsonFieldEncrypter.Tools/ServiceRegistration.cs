using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JsonFieldEncrypter.Tools
{
    public static class ServiceRegistration
    {
        public static void AddEncryption(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IFileTextProvider, FileTextProvider>();
            services.Configure<EncryptionServiceOptions>(options => configuration.GetSection(nameof(EncryptionServiceOptions)).Bind(options));
        }
    }
}