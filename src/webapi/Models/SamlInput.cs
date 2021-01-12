using System.Collections.Generic;

namespace SAMLTester.Models
{
    public class SamlInput
    {
        public string Issuer { get; set; }
        public string TargetEndpoint { get; set; }
        public List<Attribute> Attributes { get; set; }
    }

    public class Attribute
    {
        public string Name { get; set; }
        public string NameFormat { get; set; } = "urn:oasis:names:tc:SAML:2.0:attrname-format:basic";
        public string FriendlyName { get; set; }
        public string Value { get; set; }
    }
}