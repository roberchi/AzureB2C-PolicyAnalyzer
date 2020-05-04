using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class Claim : PolicyItem
    {
        public Reference<Claim> ClaimTypeReferenceId { get; private set; }
        public string DefaultValue => XmlNode.Attribute("DefaultValue").Value;
        public string PartnerClaimType => XmlNode.Attribute("PartnerClaimType").Value;

   
        public Claim()
           : base()
        { }
        public Claim(string filePath, XElement node, string id, ObjectIndex references, TechnicalProfile parent)
            : base(filePath, node, id, references, parent)
        {
             ClaimTypeReferenceId = new Reference<Claim>(this, XmlNode.Attribute("ClaimTypeReferenceId").Value, references);
        }

    }
}