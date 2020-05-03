using System;
using System.Collections.Generic;
using System.Text;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    public class Reference<T> where T : BaseItem
    {

        public Reference(BaseItem item, string id, ObjectIndex references)
        {
            Id = id;
            _references = references;
            _item = item;
        }

        public string Id { get; }

        private ObjectIndex _references;
        private BaseItem _item;

        public T GetInstance()
        {
            if (_item is Policy)
                return _references.GetPolicyReference(Id) as T;
            else
                return _references.GetReference(typeof(T), Id, ((PolicyItem)_item).Policy) as T;
        }
        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
