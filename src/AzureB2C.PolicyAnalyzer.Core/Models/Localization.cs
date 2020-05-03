using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class Localization : PolicyItem
    {
        public Localization(string filePath, XElement node, ObjectIndex references, BuildingBlocks buildingBlocks) :
            base(filePath, node, $"{buildingBlocks.Policy.Id}_{nameof(BuildingBlocks)}_{nameof(Localization)}", references, buildingBlocks)
        {
        }

        public bool Enabled => GetXmlNode().Attribute("Enabled")!= null?bool.Parse(GetXmlNode().Attribute("Enabled")?.Value):false;
        public string DefaultLanguage => GetXmlNode().Element(PolicyItem.ns + "SupportedLanguages").Attribute("DefaultLanguage")?.Value;
        public string MergeBehavior => GetXmlNode().Element(PolicyItem.ns + "SupportedLanguages").Attribute("MergeBehavior")?.Value;

        public List<string> SupportedLanguage { get; private set; }
        public List<LocalizedResources> LocalizedResources { get; private set; }
        internal static Localization Load(BuildingBlocks buildingBlocks, string path, ObjectIndex references)
        {
            var xmlNode = buildingBlocks.GetXmlNode().Element(PolicyItem.ns + "Localization");
            if (xmlNode == null)
                return null;

            Localization item = new Localization(path, xmlNode, references, buildingBlocks);
            // fill data
            item.SupportedLanguage = new List<string>();
            foreach (var lang in item.GetXmlNode().Element(PolicyItem.ns + "SupportedLanguages").Elements(PolicyItem.ns + "SupportedLanguage"))
            {
                item.SupportedLanguage.Add(lang.Value);
            }
            

            item.LocalizedResources = new List<LocalizedResources>();
            foreach (var locRes in item.GetXmlNode().Element(PolicyItem.ns + "SupportedLanguages").Elements(PolicyItem.ns + "LocalizedResources"))
            {
                item.LocalizedResources.Add(PolicyItem.Load<LocalizedResources>(item, path, locRes, references));
            }
            return item;
        }


    }
}

