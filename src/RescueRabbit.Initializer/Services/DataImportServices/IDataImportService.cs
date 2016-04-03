using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Initializer.Services.DataImportServices
{
    public interface IDataImportService
    {
        IEnumerable<TCreateRequest> Deserialize<TCreateRequest>(string fileName);

        IEnumerable<Tuple<TCreateRequest, TUpdateRequest>> Deserialize<TCreateRequest, TUpdateRequest>(string fileName);
    }
}
