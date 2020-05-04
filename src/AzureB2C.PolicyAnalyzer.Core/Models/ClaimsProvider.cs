using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class ClaimsProvider : PolicyItem
    {

        public ClaimsProvider()
            : base()
        { }
        public ClaimsProvider(string filePath, XElement node, ObjectIndex references, BaseItem parent) 
            : base(filePath, node, null, references, parent)
        {
        }

        public string DisplayName => XmlNode.Element(PolicyItem.ns + "DisplayName").Value;
        public string Domain => XmlNode.Element(PolicyItem.ns + "Domain")?.Value;
        public List<TechnicalProfile> TechnicalProfiles { get; private set; }

        internal static ClaimsProvider Load(Policy policy, string path, XElement node, ObjectIndex references)
        {
            ClaimsProvider item = new ClaimsProvider(path, node, references, policy);
            // fill data
            item.TechnicalProfiles = new List<TechnicalProfile>();
            foreach (var step in item.XmlNode.Element(PolicyItem.ns + "TechnicalProfiles").Elements(PolicyItem.ns + "TechnicalProfile"))
            {
                item.TechnicalProfiles.Add(TechnicalProfile.Load(item, path, step, references));
            }
            return item;
        }
    }
}
