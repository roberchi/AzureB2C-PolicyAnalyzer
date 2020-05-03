using System.IO;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public abstract class BaseItem
    {
        protected ObjectIndex References { get; private set; }
        public BaseItem Container { get; private set; }
        public string SourceFile{ get; private set; }

        public string Id { get; internal set; }

        private XElement xmlNode;

        public XElement GetXmlNode()
        {
            return xmlNode;
        }

        public BaseItem()
        {
            Id = "MISSING-REF";
        }
        public BaseItem(string filePath, XElement node, string id, ObjectIndex references) {
            SourceFile = filePath;
            xmlNode = node;
            Id = id;
            if(id != null)
                references.AddObject(this);
            References = references;
        }
        public BaseItem(string filePath, XElement node, string id, ObjectIndex references, BaseItem container) :
            this(filePath, node, id, references)
        {
            Container = container;
        }
    }
}