using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class TechnicalProfile : PolicyItem
    {
        public string DisplayName => GetXmlNode().Element(PolicyItem.ns + "DisplayName").Value;
        public string ProtocolName => GetXmlNode().Element(PolicyItem.ns + "Protocol").Attribute("Name").Value;
        public ClaimType SubjectNamingInfo => References.GetReference<ClaimType>(GetXmlNode().Element(PolicyItem.ns + "SubjectNamingInfo").Attribute("ClaimType").Value, this.Policy);
        
        public List<Claim> OutputClaims { get; private set; }
        public List<Claim> InputClaims { get; private set; }

        public TechnicalProfile()
           : base()
        { }
        public TechnicalProfile(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent)
            : base(filePath, node, id, references, parent)
        {
        }


        internal static TechnicalProfile Load(PolicyItem parent, string path, XElement node, ObjectIndex references)
        {
            TechnicalProfile item = new TechnicalProfile(path, node, node.Attribute("Id").Value, references, parent);
            // fill data
            item.OutputClaims = new List<Claim>();
            if (item.GetXmlNode().Element(PolicyItem.ns + "OutputClaims") != null)
            {
                foreach (var claim in item.GetXmlNode().Element(PolicyItem.ns + "OutputClaims").Elements(PolicyItem.ns + "OutputClaim"))
                {
                    item.OutputClaims.Add(PolicyItem.Load<Claim>(item, path, claim, references));
                }
            }

            item.InputClaims = new List<Claim>();
            if(item.GetXmlNode().Element(PolicyItem.ns + "InputClaims") != null)
            {
                foreach (var claim in item.GetXmlNode().Element(PolicyItem.ns + "InputClaims").Elements(PolicyItem.ns + "InputClaim"))
                {
                    item.InputClaims.Add(PolicyItem.Load<Claim>(item, path, claim, references));
                }
            }
            return item;
        }
    }
}
