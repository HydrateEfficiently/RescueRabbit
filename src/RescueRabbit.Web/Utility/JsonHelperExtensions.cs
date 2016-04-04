using Microsoft.AspNet.Mvc.Rendering;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Utility
{
    public static class JsonHelperExtensions
    {
        public static HtmlString Serialize(
            this IJsonHelper jsonHelper,
            object value,
            bool camelCasePropertyNames = false)
        {
            return camelCasePropertyNames ?
                jsonHelper.Serialize(value, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }) :
                jsonHelper.Serialize(value);

        }
    }
}
