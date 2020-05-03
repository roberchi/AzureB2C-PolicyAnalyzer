using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class ContentDefinition : PolicyItem
    {
        public string LoadUri => GetXmlNode().Element(PolicyItem.ns + "LoadUri").Value;
        public string RecoveryUri => GetXmlNode().Element(PolicyItem.ns + "RecoveryUri").Value;
        public string DataUri => GetXmlNode().Element(PolicyItem.ns + "DataUri").Value;


        public ContentDefinition()
           : base()
        { }
        public ContentDefinition(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent)
            : base(filePath, node, id, references, parent)
        {
        }
    }
}