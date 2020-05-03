using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class Policy : BaseItem
    {
        public string PolicyId => this.Id;
        public string PublicPolicyUri => GetXmlNode().Attribute("PublicPolicyUri").Value;
        public string TenantId => GetXmlNode().Attribute("TenantId").Value;
        public Reference<Policy> BasePolicy { get; private set; }
        public RelyingParty RelyingParty { get; private set; }
        public List<UserJourney> UserJourneys { get; private set; }
        public List<ClaimsProvider> ClaimsProviders { get; private set; }
        public BuildingBlocks BuildingBlocks { get; private set; }

        public Policy() : base()
        {

        }
        public Policy(string filePath, XElement node, ObjectIndex references) 
            : base(filePath, node, node.Attribute("PolicyId").Value, references)
        {
            if(GetXmlNode().Element(PolicyItem.ns + "BasePolicy")?.Element(PolicyItem.ns + "PolicyId")?.Value != null);
                BasePolicy = new Reference<Policy>(this, GetXmlNode().Element(PolicyItem.ns + "BasePolicy")?.Element(PolicyItem.ns + "PolicyId")?.Value, references);

        }


        internal static async Task<Policy> LoadXmlAsync(Stream stream, string path, ObjectIndex references)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            XDocument xml = await XDocument.LoadAsync(stream, LoadOptions.None, token);
            var policy = new Policy(path, xml.Root, references);

            // RelyingParty
            policy.RelyingParty = RelyingParty.Load(policy, path, references);

            policy.UserJourneys = new List<UserJourney>();
            foreach (var node in xml.Root.Element("UserJourneys").Elements("UserJourney"))
            {
                policy.UserJourneys.Add(UserJourney.Load(policy, path, node, references));
            }

            return policy;
        }

        internal static Policy LoadXml(string data, string path, ObjectIndex references)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            XDocument xml = XDocument.Parse(data);
            var policy = new Policy(path, xml.Root, references);

            // RelyingParty
            policy.RelyingParty = RelyingParty.Load(policy, path, references);

            // journey
            policy.UserJourneys = new List<UserJourney>();
            if (xml.Root.Element(PolicyItem.ns + "UserJourneys") != null)
            {
                foreach (var node in xml.Root.Element(PolicyItem.ns + "UserJourneys").Elements(PolicyItem.ns + "UserJourney"))
                {
                    policy.UserJourneys.Add(UserJourney.Load(policy, path, node, references));
                }
            }

            // BuildingBlocks
            policy.BuildingBlocks = BuildingBlocks.Load(policy, path, references);


            // ClaimsProviders
            policy.ClaimsProviders = new List<ClaimsProvider>();
            if (xml.Root.Element(PolicyItem.ns + "ClaimsProviders") != null)
            {
                foreach (var node in xml.Root.Element(PolicyItem.ns + "ClaimsProviders").Elements(PolicyItem.ns + "ClaimsProvider"))
                {
                    policy.ClaimsProviders.Add(ClaimsProvider.Load(policy, path, node, references));
                }
            }
            return policy;
        }

    }
}
