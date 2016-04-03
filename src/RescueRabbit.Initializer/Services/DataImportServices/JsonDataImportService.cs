using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Initializer.Services.DataImportServices
{
    public class JsonDataImportService : IDataImportService
    {
        private readonly IApplicationEnvironment _applicationEnvironment;
        private readonly DataImportOptions _options;

        public JsonDataImportService(
            IApplicationEnvironment applicationEnvironment,
            IOptions<DataImportOptions> options)
        {
            _applicationEnvironment = applicationEnvironment;
            _options = options.Value;
        }

        public IEnumerable<TCreateRequest> Deserialize<TCreateRequest>(string fileName)
        {
            return DeserializeEnumerable<TCreateRequest>(GetJsonString(fileName));
        }

        public IEnumerable<Tuple<TCreateRequest, TUpdateRequest>> Deserialize<TCreateRequest, TUpdateRequest>(string fileName)
        {
            var json = GetJsonString(fileName);
            var createRequests = DeserializeEnumerable<TCreateRequest>(json);
            var updateRequests = DeserializeEnumerable<TUpdateRequest>(json);
            return createRequests.Zip(updateRequests, (c, u) => new Tuple<TCreateRequest, TUpdateRequest>(c, u));
        }

        #region Helpers
        
        private string GetJsonString(string fileName)
        {
            var path = Path.Combine(_applicationEnvironment.ApplicationBasePath, _options.DirectoryName, $"{fileName}.json");
            return File.ReadAllText(path);
        }

        private IEnumerable<T> DeserializeEnumerable<T>(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        #endregion
    }
}
