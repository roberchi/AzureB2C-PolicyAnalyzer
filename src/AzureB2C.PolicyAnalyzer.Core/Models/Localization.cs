using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class Localization : PolicyItem
    {
        public Localization(string filePath, XElement node, ObjectIndex references, BuildingBlocks buildingBlocks) :
            base(filePath, node, null, references, buildingBlocks)
        {
        }

        public bool Enabled => XmlNode.Attribute("Enabled")!= null?bool.Parse(XmlNode.Attribute("Enabled")?.Value):false;
        public string DefaultLanguage => XmlNode.Element(PolicyItem.ns + "SupportedLanguages").Attribute("DefaultLanguage")?.Value;
        public string MergeBehavior => XmlNode.Element(PolicyItem.ns + "SupportedLanguages").Attribute("MergeBehavior")?.Value;

        public List<string> SupportedLanguage { get; private set; }
        public List<LocalizedResources> LocalizedResources { get; private set; }
        internal static Localization Load(BuildingBlocks buildingBlocks, string path, ObjectIndex references)
        {
            var xmlNode = buildingBlocks.XmlNode.Element(PolicyItem.ns + "Localization");
            if (xmlNode == null)
                return null;

            Localization item = new Localization(path, xmlNode, references, buildingBlocks);
            // fill data
            item.SupportedLanguage = new List<string>();
            foreach (var lang in item.XmlNode.Element(PolicyItem.ns + "SupportedLanguages").Elements(PolicyItem.ns + "SupportedLanguage"))
            {
                item.SupportedLanguage.Add(lang.Value);
            }
            

            item.LocalizedResources = new List<LocalizedResources>();
            foreach (var locRes in item.XmlNode.Element(PolicyItem.ns + "SupportedLanguages").Elements(PolicyItem.ns + "LocalizedResources"))
            {
                item.LocalizedResources.Add(PolicyItem.Load<LocalizedResources>(item, path, locRes, references));
            }
            return item;
        }


    }
}

