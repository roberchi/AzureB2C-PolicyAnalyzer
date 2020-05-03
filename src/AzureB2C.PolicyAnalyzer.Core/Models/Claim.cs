using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class Claim : PolicyItem
    {
        public Reference<Claim> ClaimTypeReferenceId { get; private set; }
        public string DefaultValue => GetXmlNode().Attribute("DefaultValue").Value;
        public string PartnerClaimType => GetXmlNode().Attribute("PartnerClaimType").Value;

   
        public Claim()
           : base()
        { }
        public Claim(string filePath, XElement node, string id, ObjectIndex references, TechnicalProfile parent)
            : base(filePath, node, id, references, parent)
        {
             ClaimTypeReferenceId = new Reference<Claim>(this, GetXmlNode().Attribute("ClaimTypeReferenceId").Value, references);
        }

    }
}