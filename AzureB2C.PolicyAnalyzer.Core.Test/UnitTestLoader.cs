using AzureB2C.PolicyAnalyzer.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AzureB2C.PolicyAnalyzer.Core.Test
{
    [TestClass]
    public class UnitTestLoader
    {
        [TestMethod]
        public async Task TestLoadFromGit()
        {
            ObjectIndex index = new ObjectIndex();
            string[] urls = new string[]
            {
                "https://raw.githubusercontent.com/azure-ad-b2c/samples/master/policies/change-sign-in-name/policy/TrustFrameworkExtensions.xml",
                "https://raw.githubusercontent.com/azure-ad-b2c/samples/master/policies/change-sign-in-name/policy/TrustFrameworkBase.xml",
                "https://raw.githubusercontent.com/azure-ad-b2c/samples/master/policies/change-sign-in-name/policy/SignUpOrSignin.xml",
                "https://raw.githubusercontent.com/azure-ad-b2c/samples/master/policies/change-sign-in-name/policy/ChangeSignInName.xml"
            };
            var policies = await PolicyLoader.LoadFromUrls(index, urls);
            Assert.IsTrue(policies.Count > 0);
            Assert.IsNotNull(index.GetPolicyReference("B2C_1A_ChangeSignInName"));
            Assert.IsNotNull(index.GetPolicyReference("B2C_1A_ChangeSignInName").RelyingParty);
            Assert.IsNotNull(index.GetPolicyReference("B2C_1A_ChangeSignInName").RelyingParty.DefaultUserJourney);
        }

    }
}
