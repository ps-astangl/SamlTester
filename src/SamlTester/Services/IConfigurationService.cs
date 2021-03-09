using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SAMLTester.Context;

namespace SAMLTester.Services
{
    public interface IConfigurationService
    {
        
    }

    class ConfigurationService : IConfigurationService
    {
        private readonly IRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(IRepository repository, ILogger<ConfigurationService> logger, IMemoryCache memoryCache)
        {
            _repository = repository;
            _logger = logger;
            _memoryCache = memoryCache;
        }
    }
}