using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Utility
{
    public class UrlEnumerateIgnoreAttribute : Attribute { }

    public static class UrlHelperExtensions
    {
        private const string ControllerSuffix = "Controller";
        private const string ApiSuffix = "Api";

        private static Dictionary<string, Dictionary<string, ExpandoObject>> __actionsByController =
            new Dictionary<string, Dictionary<string, ExpandoObject>>();

        public static ExpandoObject EnumerateUrls(
            this IUrlHelper urlHelper,
            params Type[] controllers)
        {
            dynamic result = new ExpandoObject();
            foreach (var controller in controllers)
            {
                if (!controller.Name.EndsWith(ControllerSuffix))
                {
                    throw new InvalidOperationException($"Expected controller name to end with \"${ControllerSuffix}\"");
                }

                string controllerName = controller.Name.Substring(0, controller.Name.Length - ControllerSuffix.Length);
                bool isApiController = controllerName.EndsWith(ApiSuffix);
                string baseControllerName = isApiController ?
                    controllerName.Substring(0, controllerName.Length - ApiSuffix.Length) :
                    controllerName;

                Dictionary<string, ExpandoObject> actionsByActionName;
                string controllerKey = isApiController ? $"{ApiSuffix}.{baseControllerName}" : baseControllerName;
                if (!__actionsByController.TryGetValue(controllerKey, out actionsByActionName))
                {
                    actionsByActionName = GetActionsByName(urlHelper, controller, controllerName);
                    __actionsByController.Add(controllerKey, actionsByActionName);
                }

                dynamic container = result;
                if (isApiController)
                {
                    if (!ExpandoObjectUtility.TryGet(result, ApiSuffix, out container))
                    {
                        container = new ExpandoObject();
                        ExpandoObjectUtility.Add(result, ApiSuffix, container);
                    }
                }
                ExpandoObjectUtility.Add(container, baseControllerName, actionsByActionName);
            }
            return result;
        }

        private static Dictionary<string, ExpandoObject> GetActionsByName(
            IUrlHelper urlHelper,
            Type controller,
            string controllerName)
        {
            var result = new Dictionary<string, ExpandoObject>();

            var publicActions = ((TypeInfo)controller).DeclaredMethods.Where(mi =>
                mi.IsPublic && !mi.IsDefined(typeof(UrlEnumerateIgnoreAttribute), false));

            foreach (var actionType in publicActions)
            {
                var routeValues = new RouteValueDictionary();
                foreach (var parameter in actionType.GetParameters().Where(pi => !pi.IsDefined(typeof(FromBodyAttribute), false)))
                {
                    routeValues.Add(parameter.Name, $":{parameter.Name}");
                }

                string actionName = actionType.Name;
                dynamic action = new ExpandoObject();
                action.UrlPattern = urlHelper.Action(actionName, controllerName, routeValues);
                action.Method = actionType.IsDefined(typeof(HttpGetAttribute)) ? "get" : "post";
                result.Add(actionName, action);
            }

            return result;
        }
    }
}
