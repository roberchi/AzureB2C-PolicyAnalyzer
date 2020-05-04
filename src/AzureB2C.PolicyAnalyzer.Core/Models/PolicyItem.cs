using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public abstract class PolicyItem : BaseItem
    {
        [Browsable(false)]
        public Policy Policy { get; private set; }
        public PolicyItem() : base()
        {
        }

        public static XNamespace ns = "http://schemas.microsoft.com/online/cpim/schemas/2013/06";


        public PolicyItem(string filePath, XElement node, string id, ObjectIndex references, BaseItem container) 
            : base(filePath, node, id, references, container)
        {
            if (container != null)
            {
                if (container is Policy)
                    Policy = (Policy)container;
                else
                    Policy = ((PolicyItem)container).Policy;
            }
        }

        internal static T Load<T>(PolicyItem parent, string path, XElement node, ObjectIndex references) where T : PolicyItem
        {
            string id = node.Attribute("Id")?.Value;
            return (T)Activator.CreateInstance(typeof(T), path, node, id, references, parent);

        }
    }
}
