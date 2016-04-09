using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Rendering;
using RescueRabbit.Services.Identity;
using RescueRabbit.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Services
{
    public class ServerDataBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserProvider _currentUserProvider;

        private Dictionary<string, object> _data = new Dictionary<string, object>();
        private bool _includeCurrentUser = false;
        private List<Type> _controllers = new List<Type>();

        public ServerDataBuilder(
            IHttpContextAccessor httpContextAccessor,
            ICurrentUserProvider currentUserProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _currentUserProvider = currentUserProvider;
        }

        public ServerDataBuilder IncludeData(string key, object value)
        {
            _data.Add(key, value);
            return this;
        }

        public ServerDataBuilder IncludeCurrentUser()
        {
            _includeCurrentUser = true;
            return this;
        }

        public ServerDataBuilder IncludeUrls(Type controllerType)
        {
            _controllers.Add(controllerType);
            return this;
        }

        public Dictionary<string, object> Build()
        {
            return BuildAsync().Result;
        }

        // TODO: Make all tasks concurrent
        public async Task<Dictionary<string, object>> BuildAsync()
        {
            await TryIncludeCurrentUserAsync();
            TryIncludeUrls();
            return _data;
        }

        public HtmlString Serialize()
        {
            return SerializeAsync().Result;
        }

        public async Task<HtmlString> SerializeAsync()
        {
            var result = await BuildAsync();
            return _httpContextAccessor.GetJsonHelper().Serialize(result, camelCasePropertyNames: true);
        }

        #region Helpers

        private async Task TryIncludeCurrentUserAsync()
        {
            if (_includeCurrentUser)
            {
                var user = await _currentUserProvider.GetAsync();
                if (user != null)
                {
                    _data.Add("User", user);
                }
            }
        }

        private void TryIncludeUrls()
        {
            if (_controllers.Count > 0)
            {
                _data.Add("Urls", _httpContextAccessor.GetUrlHelper().EnumerateUrls(_controllers.ToArray()));
            }
        }

        #endregion
    }
}
