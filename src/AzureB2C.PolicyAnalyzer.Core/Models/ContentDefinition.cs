using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class ContentDefinition : PolicyItem
    {
        public string LoadUri => XmlNode.Element(PolicyItem.ns + "LoadUri").Value;
        public string RecoveryUri => XmlNode.Element(PolicyItem.ns + "RecoveryUri").Value;
        public string DataUri => XmlNode.Element(PolicyItem.ns + "DataUri").Value;


        public ContentDefinition()
           : base()
        { }
        public ContentDefinition(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent)
            : base(filePath, node, id, references, parent)
        {
        }
    }
}