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

        private static Dictionary<string, Dictionary<string, string>> __urlsByActionByController =
            new Dictionary<string, Dictionary<string, string>>();

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

                Dictionary<string, string> actionUrlsByActionName;
                string controllerKey = isApiController ? $"{ApiSuffix}.{baseControllerName}" : baseControllerName;
                if (!__urlsByActionByController.TryGetValue(controllerKey, out actionUrlsByActionName))
                {
                    actionUrlsByActionName = GetActionUrlsByName(urlHelper, controller, controllerName);
                    __urlsByActionByController.Add(controllerKey, actionUrlsByActionName);
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
                ExpandoObjectUtility.Add(container, baseControllerName, actionUrlsByActionName);
            }
            return result;
        }

        private static Dictionary<string, string> GetActionUrlsByName(
            IUrlHelper urlHelper,
            Type controller,
            string controllerName)
        {
            var result = new Dictionary<string, string>();

            var publicActions = ((TypeInfo)controller).DeclaredMethods.Where(mi =>
                mi.IsPublic && !mi.IsDefined(typeof(UrlEnumerateIgnoreAttribute), false));

            foreach (var action in publicActions)
            {
                string actionName = action.Name;
                var routeValues = new RouteValueDictionary();
                foreach (var parameter in action.GetParameters().Where(pi => !pi.IsDefined(typeof(FromBodyAttribute), false)))
                {
                    routeValues.Add(parameter.Name, $":{parameter.Name}");
                }
                result.Add(actionName, urlHelper.Action(actionName, controllerName, routeValues));
            }

            return result;
        }
    }
}
