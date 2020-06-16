using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace PampaDevs.Utils.Extensions
{
    public static class DynamicExtensions
    {
        public static dynamic ToDynamic(this object src)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            
            foreach(PropertyDescriptor property in TypeDescriptor.GetProperties(src.GetType()))
            {
                expando.Add(property.Name, property.GetValue(src));
            }

            return expando as ExpandoObject;
        }
    }
}
