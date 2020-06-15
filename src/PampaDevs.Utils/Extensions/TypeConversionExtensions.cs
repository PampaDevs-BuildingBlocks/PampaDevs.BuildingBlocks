using System;
using System.ComponentModel;
using System.Diagnostics;

namespace PampaDevs.Utils.Extensions
{
    public static class TypeConversionExtensions
    {
        [DebuggerStepThrough]
        public static T ConvertTo<T>(this string src)
        {            
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(src);
            }
            catch(NotSupportedException)
            {
                return default;
            }
        }
    }
}