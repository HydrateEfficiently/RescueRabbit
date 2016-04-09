using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Utility
{
    public static class ExpandoObjectUtility
    {
        public static void Add(ExpandoObject expando, string key, object value)
        {
            ((IDictionary<string, object>)expando)[key] = value;
        }

        public static bool TryGetRecursive<TValue>(ExpandoObject expando, string key, out TValue value)
        {
            var keys = key.Split('.');
            var current = expando;
            if (keys.Length > 1)
            {
                foreach (var k in keys)
                {
                    if (!TryGet(current, k, out current))
                    {
                        value = default(TValue);
                        return false;
                    }
                }
            }
            return TryGet(expando, keys.Last(), out value);
        }

        public static bool TryGet<TValue>(ExpandoObject expando, string key, out TValue value)
        {
            var dictionary = (IDictionary<string, object>)expando;
            object valueBeforeCast;
            if (dictionary.TryGetValue(key, out valueBeforeCast))
            {
                value = (TValue)valueBeforeCast;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
    }
}
