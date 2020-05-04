using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{

    public class OrchestrationStep : PolicyItem
    {
        public int Order => int.Parse(XmlNode.Attribute("Order").Value);
        public string Type => XmlNode.Attribute("Type").Value;

        public Reference<ContentDefinition> ContentDefinition { get; private set; }
        public Reference<TechnicalProfile> CpimIssuerTechnicalProfile { get; private set; }
        
        public OrchestrationStep(string filePath, XElement node, ObjectIndex references, BaseItem parent) 
            : base(filePath, node, null, references, parent)
        {
            if (XmlNode.Attribute(PolicyItem.ns + "ContentDefinitionReferenceId") != null)
                ContentDefinition = new Reference<ContentDefinition>(this, XmlNode.Attribute(PolicyItem.ns + "ContentDefinitionReferenceId").Value, references);
            if (XmlNode.Attribute(PolicyItem.ns + "CpimIssuerTechnicalProfileId") != null)
                CpimIssuerTechnicalProfile = new Reference<TechnicalProfile>(this, XmlNode.Attribute(PolicyItem.ns + "CpimIssuerTechnicalProfileId").Value, references);

        }


        internal static OrchestrationStep Load(UserJourney journey, string path, XElement node, ObjectIndex references)
        {
            OrchestrationStep item = new OrchestrationStep(path, node, references, journey);
            // fill data

            return item;
        }
    }
}
