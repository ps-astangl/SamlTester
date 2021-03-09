using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SAMLTester.Context
{
    public class PartnerServiceProviderConfiguration : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool SignSamlResponse { get; set; }
        public bool EncryptAssertion { get; set; }
        public string AssertionConsumerServiceUrl { get; set; }
        public IEnumerable<PartnerCertificate> PartnerCertificates { get; set; }
    }

    public class PartnerCertificate : BaseEntity
    {
        [JsonIgnore] public virtual Guid PartnerServiceProviderConfigurationId { get; set; }

        [JsonIgnore]
        public virtual PartnerServiceProviderConfiguration PartnerServiceProviderConfiguration { get; set; }

        public string Thumbprint { get; set; }
        public string String { get; set; }
    }

    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }

    public class PartnerServiceProviderConfigurationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool SignSamlResponse { get; set; }
        public bool EncryptAssertion { get; set; }
        public string AssertionConsumerServiceUrl { get; set; }
        public IEnumerable<PartnerCertificateDTO> PartnerCertificates { get; set; }
    }

    public class PartnerCertificateDTO
    {
        public string Thumbprint { get; set; }
        public string String { get; set; }
    }

}