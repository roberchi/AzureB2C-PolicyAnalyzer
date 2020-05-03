using AzureB2C.PolicyAnalyzer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureB2C.PolicyAnalyzer.Core
{
    static class Helper
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f) => e.SelectMany(c => f(c).Flatten(f)).Concat(e);

        public static List<Policy> Inheritances(this Policy root)
        {
            List<Policy> inheritance = new List<Policy>();
            if (root.BasePolicy != null)
                inheritance.AddRange(root.BasePolicy.GetInstance().Inheritances());
            
            inheritance.Add(root);
            return inheritance;
        }

        public static T CreateUnkReference<T>(string id) where T : BaseItem, new()
        {
            T unkReference = new T(); // return unknow reference
            unkReference.Id = $"UNKNOW-REF-{id}";
            return unkReference;
        }

        public static T CreateNullReference<T>() where T : BaseItem, new()
        {
            T unkReference = new T(); // return unknow reference
            unkReference.Id = $"NULL-REF";
            return unkReference;
        }
    }
}
