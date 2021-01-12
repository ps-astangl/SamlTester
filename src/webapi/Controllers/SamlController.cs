using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentSpace.Saml2;
using ComponentSpace.Saml2.Assertions;
using ComponentSpace.Saml2.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SAMLTester.Models;

namespace SAMLTester.Controllers
{
    [ApiController]
    [Route("api")]
    public class SamlController : ControllerBase
    {
        private ILogger<SamlController> _logger;
        private readonly ISamlIdentityProvider _samlIdentityProvider;
        private readonly SamlConfigurations _samlConfigurations;
        public SamlController(
            ILogger<SamlController> logger,
            ISamlIdentityProvider samlIdentityProvider,
            IOptionsSnapshot<SamlConfigurations> samlConfigurations)
        {
            _logger = logger;
            _samlIdentityProvider = samlIdentityProvider;
            _samlConfigurations = samlConfigurations.Value;
        }

        [HttpPost]
        public async Task SignSaml([FromBody] SamlInput input)
        {
            UpdateSamlConfigs(input);

            List<SamlAttribute> attributes = input.Attributes.Select(
                    attr => new SamlAttribute(attr.Name, attr.NameFormat, attr.FriendlyName, attr.Value)
                ).ToList();

            await _samlIdentityProvider.InitiateSsoAsync(
                partnerName: input.Issuer,
                attributes: attributes
            );
        }

        private void UpdateSamlConfigs(SamlInput input)
        {
            var samlConfiguration = _samlConfigurations.Configurations.FirstOrDefault();

            var cert = samlConfiguration.LocalIdentityProviderConfiguration.LocalCertificates;
            samlConfiguration.LocalIdentityProviderConfiguration = new LocalIdentityProviderConfiguration()
                {
                    Name = input.Issuer,
                    Description = "Example Identity Provider 2",
                    SingleSignOnServiceUrl = input.TargetEndpoint,
                    LocalCertificates = cert
                };

            samlConfiguration.PartnerServiceProviderConfigurations.Add(new PartnerServiceProviderConfiguration()
            {
                Name = input.Issuer,
                Description = "Example Service Provider",
                SingleLogoutServiceUrl = input.TargetEndpoint,
                LocalCertificates = cert
            });
        }
    }
}