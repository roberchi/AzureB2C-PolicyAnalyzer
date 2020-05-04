using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class UserJourney : PolicyItem
    {

        public UserJourney()
            :base()
        { }
        public UserJourney(string filePath, XElement node, string id, ObjectIndex references, BaseItem parent) : base(filePath, node, id, references, parent)
        {
        }

        public List<OrchestrationStep> OrchestrationSteps { get; private set; }

        internal static UserJourney Load(Policy policy, string path, XElement node, ObjectIndex references)
        {
            UserJourney item = new UserJourney(path, node, node.Attribute("Id").Value, references, policy);
            // fill data
            item.OrchestrationSteps = new List<OrchestrationStep>();
            foreach (var step in item.XmlNode.Element(PolicyItem.ns + "OrchestrationSteps").Elements(PolicyItem.ns + "OrchestrationStep"))
            {
                item.OrchestrationSteps.Add(OrchestrationStep.Load(item, path, step, references));
            }
            return item;
        }
    }
}
