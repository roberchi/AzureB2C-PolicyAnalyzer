using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class ObjectIndex
    {
        List<BaseItem> index = new List<BaseItem>();

        public void AddObject(BaseItem item) 
        {
            index.Add(item);
        }

        public Policy GetPolicyReference(string id)
        {
            var allReference = index.Where(i => i is Policy && i.Id == id);
            if (allReference.Count() == 1)
                return (Policy)allReference.First();
            else
                return Helper.CreateUnkReference<Policy>(id);
        }

        public T GetReference<T>(string id, Policy startPolicy) where T : BaseItem, new()
        {
            if (typeof(T) == typeof(Policy))
                return GetPolicyReference(id) as T;
            else
                return GetReference(typeof(T), id, startPolicy) as T;

        }
        public T GetItemReference<T>(string id, Policy startPolicy) where T : PolicyItem, new()
        {
            if (id == null)
                return Helper.CreateNullReference<T>();
            var allReference = index.Where(i => i.GetType() == typeof(T) && i.Id == id).Cast<PolicyItem>();

            // return policy item
            if (allReference.Count() == 1)
            {
                PolicyItem reference = (PolicyItem)allReference.First();
                if (startPolicy.Inheritances().Contains(reference.Policy))
                    return (T)allReference.First();
                else
                    return Helper.CreateUnkReference<T>(id); // reference is not in any policy 
            }
            else if (allReference.Count() == 0)
                return Helper.CreateUnkReference<T>(id);
            else
            {
                var pi = startPolicy.Inheritances();
                // filter all references in the inheritance chain
                var refFiltered = allReference.Where(r => pi.Contains(r.Policy));
                return (T)refFiltered.OrderBy(r => pi.IndexOf(r.Policy)).First();
            }
        }

        public object GetReference(Type type, string id, Policy startPolicy) 
        {
            if(id == null)
                return null;
            var allReference = index.Where(i => i.GetType() == type && i.Id == id).Cast<PolicyItem>();

            // return policy item
            if (allReference.Count() == 1)
            {
                PolicyItem reference = (PolicyItem)allReference.First();
                if (startPolicy.Inheritances().Contains(reference.Policy))
                    return allReference.First();
                else
                    return null; // reference is not in any policy 
            }
            else if (allReference.Count() == 0)
                return null;
            else
            {
                var pi = startPolicy.Inheritances();
                // filter all references in the inheritance chain
                var refFiltered = allReference.Where(r => pi.Contains(r.Policy));
                return refFiltered.OrderBy(r => pi.IndexOf(r.Policy)).First();
            }
        }

        

    }
}
