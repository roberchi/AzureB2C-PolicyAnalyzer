using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class ClaimType : PolicyItem
    {
        public string DisplayName => GetXmlNode().Element(PolicyItem.ns + "DisplayName").Value;
        public string DataType => GetXmlNode().Element(PolicyItem.ns + "DataType").Value;
        public string UserHelpText => GetXmlNode().Element(PolicyItem.ns + "UserHelpText").Value;

        
        public ClaimType()
           : base()
        { }
        public ClaimType(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent)
            : base(filePath, node, id, references, parent)
        {
        }
    }
}