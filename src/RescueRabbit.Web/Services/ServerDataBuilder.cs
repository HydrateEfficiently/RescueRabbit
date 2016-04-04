using RescueRabbit.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Services
{
    public class ServerDataBuilder
    {
        private readonly ICurrentUserProvider _currentUserProvider;

        private bool _includeCurrentUser;

        public ServerDataBuilder(
            ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
        }

        public ServerDataBuilder AddCurrentUser()
        {
            _includeCurrentUser = true;
            return this;
        }

        public Dictionary<string, object> Build()
        {
            return BuildAsync().Result;
        }

        // TODO: Make all tasks concurrent
        public async Task<Dictionary<string, object>> BuildAsync()
        {
            var data = new Dictionary<string, object>();

            if (_includeCurrentUser)
            {
                data.Add("User", await _currentUserProvider.GetAsync());
            }

            return data;
        }
    }
}
