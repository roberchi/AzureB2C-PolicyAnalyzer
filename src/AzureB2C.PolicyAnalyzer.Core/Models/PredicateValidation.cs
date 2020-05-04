using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class PredicateValidation : PolicyItem
    {
        public PredicateValidation()
           : base()
        { }
        public PredicateValidation(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent)
            : base(filePath, node, id, references, parent)
        {
        }
    }


    public class Predicate : PolicyItem
    {
        public string Method => XmlNode.Attribute("Method").Value;

        public Predicate()
           : base()
        { }
        public Predicate(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent)
            : base(filePath, node, id, references, parent)
        {
        }
    }
}