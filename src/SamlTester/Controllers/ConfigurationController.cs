using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAMLTester.Context;

namespace SAMLTester.Controllers
{
    [Route("[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IRepository _repository;

        public ConfigurationController(IRepository repository, ILogger<ConfigurationController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SetConfiguration(PartnerServiceProviderConfigurationDTO configurationDto)
        {
            return NotFound("Endpoint Not Active");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConfigurations(PartnerServiceProviderConfigurationDTO configurationDto)
        {
            var result = await _repository.ListAllConfigurations();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetConfigurationByName()
        {
            return NotFound("Endpoint Not Active");
        }
    }
}