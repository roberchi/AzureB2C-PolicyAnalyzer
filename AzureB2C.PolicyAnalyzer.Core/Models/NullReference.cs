using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace AzureB2C.PolicyAnalyzer.Core.Models
{
    [Obsolete("don't use", true)]
    public class NullReference : PolicyItem
    {
        public NullReference() : base(null, null, "REF_NOT_FOUND", null, null)
        {
        }

    }
}
