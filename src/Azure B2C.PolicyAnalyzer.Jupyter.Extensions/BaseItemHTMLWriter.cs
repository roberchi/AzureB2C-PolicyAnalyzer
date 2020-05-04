using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using System.Linq;
using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;
using AzureB2C.PolicyAnalyzer.Core.Models;
using System.ComponentModel;
using System.Reflection;

namespace Azure_B2C.PolicyAnalyzer.Jupyter.Extensions
{
    public static class BaseItemHTMLWriter
    {
        public static IHtmlContent DrawItem(this BaseItem item, bool root = true) //where T : BaseItem
        {
             var members = item.GetType().GetProperties();
            IEnumerable<object> values = members.Where(m=> 
                IsBrowsable(m, item) && // browsable
                DisplayId(m, item, root) && 
                DisplaySourceFile(m, item, root) &&
                NotNull(m, item)
            )
                .OrderBy(m => m.Name)
                .Select(m => tr(td(m.Name), td(ValueToHtml(m.GetValue(item)))));

            if (root)
            {
                return div[id: item.Id](
                    table(
                        thead(
                            tr(
                                th("Property"), th("Value"))
                            ),
                        tbody(
                            tr(
                                values)
                            )
                        )
                    );
            }
            else
            {
                return div[id: item.Id](
                                    table(
                                        tbody(
                                            tr(
                                                values)
                                            )
                                        )
                                    );
            }
        }

        private static object GetCategory(PropertyInfo m)
        {
            CategoryAttribute category = m.GetCustomAttributes(typeof(CategoryAttribute), true).Cast<CategoryAttribute>().FirstOrDefault();
            return category != null ? category.Category : "[Default]";
        }

        private static bool NotNull(PropertyInfo m, BaseItem item)
        {
            return m.GetValue(item) != null;
        }

        private static bool DisplaySourceFile(PropertyInfo m, BaseItem item, bool root)
        {
            if (m.Name != "SourceFile") // apply only on SourceFile property
                return true;
            return root;
        }

        private static bool DisplayId(PropertyInfo m, BaseItem item, bool root)
        {
            if (m.Name != "Id") // apply only on Id property
                return true;
            if (root)
                return true;
            if (m.GetValue(item) != null)
                return true;
            return false;
        }

        private static bool IsBrowsable(PropertyInfo m, BaseItem item)
        {
            BrowsableAttribute browsable = m.GetCustomAttributes(typeof(BrowsableAttribute), true).Cast<BrowsableAttribute>().FirstOrDefault();
            return browsable!=null ? browsable.Browsable : true;
        }

        private static IHtmlContent ValueToHtml(object value)
        {
            if (value == null)
                return span(i("null"));

            if (value is BaseItem)
                return DrawItem((BaseItem)value, false);
            else
                return span(value.ToString());
        }
    }
}
