using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class PolicyLoader
    {
        public static async Task<Policy> LoadFromStream(Stream stream, string fileName, ObjectIndex objectIndex)
        {
            return await Policy.LoadXmlAsync(stream, fileName, objectIndex);
        }
        public static async Task<List<Policy>> LoadFromUrls(ObjectIndex objectIndex, params string[] ulrs)
        {
            var policies = ulrs.Select(url =>
            {
                return LoadFromUrl(url, objectIndex);
            });
            var result = await Task.WhenAll(policies);
            var allPolicy = result.ToList();
            return allPolicy;
        }

        static async Task<Policy> LoadFromUrl(string url, ObjectIndex objectIndex)
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync(url);
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var policy = await resp.Content.ReadAsStringAsync();
                return Policy.LoadXml(policy, url, objectIndex);
            }
            else
                return null;
        }


        public static async Task<List<Policy>> LoadFromFiles(ObjectIndex objectIndex, params string[] files)
        {
            var policies = files.Select(path =>
            {
                using Stream fileStream = File.OpenRead(path);
                return Policy.LoadXmlAsync(fileStream, path, objectIndex);
            });
            var result = await Task.WhenAll(policies);
            var allPolicy = result.ToList();
            return allPolicy;
        }

        public static async Task<List<Policy>> LoadFromFolder(string folder, ObjectIndex objectIndex)
        {
            var policies = Directory.EnumerateFiles(folder, "*.xml").Select(path =>
            {
                using Stream fileStream = File.OpenRead(path);
                return Policy.LoadXmlAsync(fileStream, path, objectIndex);
            });
            var result = await Task.WhenAll(policies);
            var allPolicy = result.ToList();
            return allPolicy;
        }

        
    }
}
