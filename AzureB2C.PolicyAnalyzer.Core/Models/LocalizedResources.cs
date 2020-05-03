using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class LocalizedResources : PolicyItem
    {
        
        public LocalizedResources()
           : base()
        { }
        public LocalizedResources(string filePath, XElement node, string id, ObjectIndex references, Localization parent)
            : base(filePath, node, id, references, parent)
        {
        }
    }
}