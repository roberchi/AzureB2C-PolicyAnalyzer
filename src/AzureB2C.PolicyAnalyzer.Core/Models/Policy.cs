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
        public string PublicPolicyUri => XmlNode.Attribute("PublicPolicyUri").Value;
        public string TenantId => XmlNode.Attribute("TenantId").Value;
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
            if(XmlNode.Element(PolicyItem.ns + "BasePolicy")?.Element(PolicyItem.ns + "PolicyId")?.Value != null)
                BasePolicy = new Reference<Policy>(this, XmlNode.Element(PolicyItem.ns + "BasePolicy")?.Element(PolicyItem.ns + "PolicyId")?.Value, references);

            if (XmlNode.Element("UserJourneys") != null)
            {
                this.UserJourneys = new List<UserJourney>();
                foreach (var uj in XmlNode.Element("UserJourneys").Elements("UserJourney"))
                {
                    this.UserJourneys.Add(UserJourney.Load(this, filePath, uj, references));
                }
            }

            // RelyingParty
            this.RelyingParty = RelyingParty.Load(this, filePath, references);
            // BuildingBlocks
            this.BuildingBlocks = BuildingBlocks.Load(this, filePath, references);

            // ClaimsProviders
            if (XmlNode.Element(PolicyItem.ns + "ClaimsProviders") != null)
            {
                this.ClaimsProviders = new List<ClaimsProvider>();
                foreach (var cp in XmlNode.Element(PolicyItem.ns + "ClaimsProviders").Elements(PolicyItem.ns + "ClaimsProvider"))
                {
                    this.ClaimsProviders.Add(ClaimsProvider.Load(this, filePath, cp, references));
                }
            }


        }


        internal static async Task<Policy> LoadXmlAsync(Stream stream, string path, ObjectIndex references)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            XDocument xml = await XDocument.LoadAsync(stream, LoadOptions.None, token);
            var policy = new Policy(path, xml.Root, references);
            return policy;
        }

        internal static Policy LoadXml(string data, string path, ObjectIndex references)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            XDocument xml = XDocument.Parse(data);
            var policy = new Policy(path, xml.Root, references);
            return policy;
        }

    }
}
