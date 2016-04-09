using RescueRabbit.Framework.Services;
using System.IO;
using System.Threading.Tasks;

namespace Azn.RiskApp.Web.Services.Framework._Dev
{
    public class DevCsvEmailService : IEmailService
    {
        public const string FilePath = "C:/Users/Michael/_DevCsvEmailService.csv";

        public Task SendEmailAsync(string email, string subject, string message)
        {
            File.AppendAllLines(FilePath, new[] { $"{email},{subject},{message}" });
            return Task.FromResult(0);
        }
    }
}