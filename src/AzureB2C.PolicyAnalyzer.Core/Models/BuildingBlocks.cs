using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class BuildingBlocks : PolicyItem
    {
        public BuildingBlocks() : base()
        {
        }

        public BuildingBlocks(string filePath, XElement node, ObjectIndex references, Policy policy) :
            base(filePath, node, null, references, policy)
        {
        }

        public List<ClaimType> ClaimsSchema { get; private set; }
        public List<Predicate> Predicates { get; private set; }
        public List<PredicateValidation> PredicateValidations { get; private set; }
        public List<ContentDefinition> ContentDefinitions { get; private set; }
        public Localization Localization { get; private set; }

        internal static BuildingBlocks Load(Policy policy, string path, ObjectIndex references)
        {
            var xmlNode = policy.XmlNode.Element(PolicyItem.ns + "BuildingBlocks");
            if (xmlNode == null)
                return null;

            BuildingBlocks item = new BuildingBlocks(path, xmlNode, references, policy);
            // fill data
            
            // claims schema
            item.ClaimsSchema = new List<ClaimType>();
            if (xmlNode.Element(PolicyItem.ns + "ClaimsSchema") != null)
            {
                foreach (var node in xmlNode.Element(PolicyItem.ns + "ClaimsSchema").Elements(PolicyItem.ns + "ClaimType"))
                {
                    item.ClaimsSchema.Add(PolicyItem.Load<ClaimType>(item, path, node, references));
                }
            }

            // Predicates
            item.Predicates = new List<Predicate>();
            if (xmlNode.Element(PolicyItem.ns + "Predicates") != null)
            {
                // Predicate
                foreach (var node in xmlNode.Element(PolicyItem.ns + "Predicates").Elements(PolicyItem.ns + "Predicate"))
                {
                    item.Predicates.Add(PolicyItem.Load<Predicate>(item, path, node, references));
                }
            }

            // PredicateValidations
            item.PredicateValidations = new List<PredicateValidation>();
            if (xmlNode.Element(PolicyItem.ns + "PredicateValidations") != null)
            {
                foreach (var node in xmlNode.Element(PolicyItem.ns + "PredicateValidations").Elements(PolicyItem.ns + "PredicateValidation"))
                {
                    item.PredicateValidations.Add(PolicyItem.Load<PredicateValidation>(item, path, node, references));
                }
            }

            // ContentDefinitions
            item.ContentDefinitions = new List<ContentDefinition>();
            if (xmlNode.Element(PolicyItem.ns + "ContentDefinitions") != null)
            {
                foreach (var node in xmlNode.Element(PolicyItem.ns + "ContentDefinitions").Elements(PolicyItem.ns + "ContentDefinition"))
                {
                    item.ContentDefinitions.Add(PolicyItem.Load<ContentDefinition>(item, path, node, references));
                }
            }

            // ContentDefinitions
            item.Localization = Localization.Load(item, path, references);
            

            return item;
        }


    }
}
