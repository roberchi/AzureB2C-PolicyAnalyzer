using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class RelyingParty : PolicyItem
    {
        public RelyingParty(string filePath, XElement node, ObjectIndex references, Policy policy) :
            base(filePath, node, null, references, policy)
        {
            if(XmlNode.Element(PolicyItem.ns + "DefaultUserJourney")?.Attribute("ReferenceId")?.Value != null)
                DefaultUserJourney = new Reference<UserJourney>(this, XmlNode.Element(PolicyItem.ns + "DefaultUserJourney")?.Attribute("ReferenceId")?.Value, references); 
        }

        public Reference<UserJourney> DefaultUserJourney { get; private set; }
        
        internal static RelyingParty Load(Policy policy, string path, ObjectIndex references)
        {
            var xmlNode = policy.XmlNode.Element(PolicyItem.ns + "RelyingParty");
            if(xmlNode == null)
                return null;
            
            RelyingParty item = new RelyingParty(path, xmlNode, references, policy);
            // fill data
            return item;
        }

    
    }
}
