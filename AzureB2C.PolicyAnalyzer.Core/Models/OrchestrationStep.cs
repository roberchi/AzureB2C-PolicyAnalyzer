using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{

    public class OrchestrationStep : PolicyItem
    {
        public int Order => int.Parse(GetXmlNode().Attribute("Order").Value);
        public string Type => GetXmlNode().Attribute("Type").Value;

        public Reference<ContentDefinition> ContentDefinition { get; private set; }
        public Reference<TechnicalProfile> CpimIssuerTechnicalProfile { get; private set; }
        
        public OrchestrationStep(string filePath, XElement node, ObjectIndex references, BaseItem parent) 
            : base(filePath, node, $"{parent.Id}_{nameof(OrchestrationStep)}_{node.Attribute("Order").Value}", references, parent)
        {
            if (GetXmlNode().Attribute(PolicyItem.ns + "ContentDefinitionReferenceId") != null)
                ContentDefinition = new Reference<ContentDefinition>(this, GetXmlNode().Attribute(PolicyItem.ns + "ContentDefinitionReferenceId").Value, references);
            if (GetXmlNode().Attribute(PolicyItem.ns + "CpimIssuerTechnicalProfileId") != null)
                CpimIssuerTechnicalProfile = new Reference<TechnicalProfile>(this, GetXmlNode().Attribute(PolicyItem.ns + "CpimIssuerTechnicalProfileId").Value, references);

        }


        internal static OrchestrationStep Load(UserJourney journey, string path, XElement node, ObjectIndex references)
        {
            OrchestrationStep item = new OrchestrationStep(path, node, references, journey);
            // fill data

            return item;
        }
    }
}
