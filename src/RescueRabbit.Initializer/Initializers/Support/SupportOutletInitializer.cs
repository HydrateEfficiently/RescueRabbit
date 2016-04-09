using RescueRabbit.Framework.Models.Identity;
using RescueRabbit.Framework.Utility;
using RescueRabbit.Initializer.Initializers.Identity;
using RescueRabbit.Initializer.Services.DataImportServices;
using RescueRabbit.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Initializer.Initializers.Support
{
    public class SupportOutletInitializer : IDataInitializer
    {
        private readonly ISupportService _supportOutletService;
        private readonly JsonDataImportService _jsonDataImportService;

        public SupportOutletInitializer(
            ISupportService supportOutletService,
            JsonDataImportService jsonDataImportService)
        {
            _supportOutletService = supportOutletService;
            _jsonDataImportService = jsonDataImportService;
        }

        public void Run()
        {
            _jsonDataImportService
                .Deserialize<CreateSupportOutletRequest>("support-outlets")
                .ForEach(so => {
                    var existing = _supportOutletService.GetAsync(so.Id).Result;
                    if (existing == null)
                    {
                        _supportOutletService.CreateAsync(so).Wait();
                    }
                });
        }
    }
}
